using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    [Header("AI Movement")]
    public List<GameObject> wayPoints;
    public bool useRandomMovement;

    [Header("Detection Settings")]
    public float hearingDistance = 10f;
    public float sightDistance = 30f;
    public float smellDuration;
    
    [Header("AI Attributes")]
    public float health = 100f;
    public float weaponDamage;
    public int magazineSize;
    public int currentAmmo;
    
    [Header("AI Type")]
    [SerializeField] private AIType aiType;
    //[SerializeField] private AnimatorController[] animatorControllers;

    private enum AIType
    {
        TheHunter,
    };
    
    private Animator _animator;
    private bool _isPatroling = true;
    private AnimatorStateInfo _animationStateInfo;
    private int _WPCount = 0;
    private GameObject _player;
    private NavMeshAgent _NavMeshAgent;
    private float shootingTimer;
    private Vector3 randTarget;
    private bool _isCheckingResources;
    private Vector3 _ambushPos;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        InitializeAIType();
        
        _player = GameObject.Find("FPSController");
        _NavMeshAgent = GetComponent<NavMeshAgent>();

        _animator.SetTrigger("startPatrol");

        currentAmmo = magazineSize;
        
        _animator.SetFloat("health", health);
        
        randTarget = transform.position;

        if (wayPoints != null)
        {
            foreach (var wp in wayPoints)
            {
                wp.GetComponent<MeshRenderer>().enabled = false;
            }
        }

    }

    private void InitializeAIType()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _animationStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        Listen();
        Sight();

        if (_animationStateInfo.IsName("FollowPlayer"))
        {
            _NavMeshAgent.SetDestination(_player.transform.position);
        }

        //Patrolling
        if (_animationStateInfo.IsName("Patrol"))
        {
            if (!useRandomMovement)
            {
                var target = wayPoints[_WPCount];
                if (Vector3.Distance(transform.position, target.transform.position) < 1.0)
                {
                    MoveToNextWP();
                }
                _NavMeshAgent.SetDestination(target.transform.position);
            }
            else
            {
                if (aiType == AIType.TheHunter)
                {
                    if (Vector3.Distance(transform.position, randTarget) < 1.0)
                    {
                        randTarget = MoveTowardsPlayer();
                        Debug.Log(randTarget);
                    }
                    _NavMeshAgent.SetDestination(randTarget);
                }
            }
        }
        
        //Fleeing
        if (_animationStateInfo.IsName("Flee"))
        {
            if (Vector3.Distance(transform.position, randTarget) < 1.0)
            {
                randTarget = Flee();
            }
            _NavMeshAgent.SetDestination(randTarget);
        }

        //Death
        if (_animationStateInfo.IsName("Death"))
        {
            _NavMeshAgent.isStopped = true;
            Destroy(gameObject,8f);
        }

        //Smell Counter
        if (smellDuration > 0f)
        {
            smellDuration -= Time.deltaTime;
        }
        else _animator.SetBool("canSmellPlayer", false);
    }

    private void Sight()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 1.5f;
        string objectInSight;

        ray.direction = transform.forward * sightDistance;
        Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, sightDistance))
        {
            objectInSight = hit.collider.gameObject.name;
            if (objectInSight == "FPSController")
            {
                _animator.SetBool("canSeePlayer",true);
            }
            else
            {
                _animator.SetBool("canSeePlayer",false);
            }
        }
        else _animator.SetBool("canSeePlayer",false);
    }

    private void Listen()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance <= hearingDistance)
        {
            _animator.SetBool("canHearPlayer",true);
        }
        else _animator.SetBool("canHearPlayer",false);
    }

    private void Smell()
    {
        smellDuration = 3f;
        _animator.SetBool("canSmellPlayer", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BreadCrumb"))
        {
            Smell();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _animator.SetBool("wasShot",true);
        
        health -= damageAmount;
        _animator.SetFloat("health", health);

        StartCoroutine("StopFollowingPlayer");
    }

    private IEnumerator StopFollowingPlayer()
    {
        yield return new WaitForSeconds(10f);
        
        _animator.SetBool("wasShot",false);
        
        yield return null;
    }

    private Vector3 MoveToRandomWP()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        float distanceToObstacle = 0;
        float castingDistance = 20;
        do
        {
            float randomDirectionX = Random.Range(-1, 1);
            float randomDirectionZ = Random.Range(-1, 1);
            ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
            {
                distanceToObstacle = hit.distance;
            }
            else distanceToObstacle = castingDistance;
            
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
            
            //Debug.Log(ray.origin + ray.direction * (distanceToObstacle - 1));
            //Debug.DrawLine(transform.position, ray.origin + ray.direction * (distanceToObstacle - 5f), Color.blue,5f);
            return ray.origin + ray.direction * (distanceToObstacle - 5f);
            
        } while (distanceToObstacle < 1.0f);
    }

    private void MoveToNextWP()
    {
        _WPCount++;
        if (_WPCount > wayPoints.Count - 1)
        {
            _WPCount = 0;
        }
    }

    private Vector3 Flee()
    {
        var nextDest = MoveToRandomWP();

        if (Vector3.Distance(nextDest, _player.transform.position) < 20)
        {
            nextDest = nextDest + (_player.transform.position * -1);

            return nextDest;
        }
        return nextDest;
    }

    private Vector3 MoveTowardsPlayer()
    {
        var nextDest = MoveToRandomWP();
        
        nextDest = Vector3.Lerp(nextDest, _player.transform.position,0.35f);
        
        return nextDest;
    }
    
    private T FindByName<T>(T[] arr, string nameString)
    {
        foreach (var a in arr)
        {
            if (a.ToString().Replace(" (UnityEngine.AnimatorController)", "") == nameString)
            {
                return a;
            }
        }

        return arr[0];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,hearingDistance/2);
    }
}
