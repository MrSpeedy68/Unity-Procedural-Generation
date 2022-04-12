using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class GenerateProceduralMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;

    public int xSize = 20;
    public int zSize = 20;

    public float perlinHeight = 10f;
    public float perlinZoom = 0.05f;

    public GameObject coin;
    public int spawnAmount;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void PlacePickUps(Vector3 position)
    {
        int rnd = Random.Range(0, 10000);

        if (rnd == spawnAmount && spawnAmount > 0)
        {
            Instantiate(coin, position, Quaternion.identity);
            spawnAmount--;
        }
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uv = new Vector2[(xSize + 1) * (zSize + 1)];
        
        for (int i = 0, z =0; z<= zSize; z++)
        {
            for (int x = 0; x<=xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * perlinZoom, z * perlinZoom) * perlinHeight;
                vertices[i] = new Vector3(x, y, z);
                if (spawnAmount > 0) PlacePickUps(new Vector3(x,y + 2f,z));
                uv[i] = new Vector2(z / (float)zSize, x / (float)xSize);
                
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;


        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
        
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // private void OnDrawGizmos()
    // {
    //     if (vertices == null)
    //         return;
    //     
    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(vertices[i], .1f);
    //     }
    // }
}
