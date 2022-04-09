using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    const int N = 1, S = 2, E = 3, W = 4;
    int[,] grid;
    [SerializeField] [Range(5, 100)] int width, heigth;

    [SerializeField] [Range(5, 100)] int wallSize;

    public GameObject verticalWall, horizontalWall;

    GameObject[,] gridObjectsH, gridObjectsV;
    GameObject[] allObjectsInScene;

    float wallHeight;

    private void Init()
    {
        heigth = width;
        wallHeight = 4;
        verticalWall.transform.localScale = new Vector3(.1f, wallHeight, wallSize);
        horizontalWall.transform.localScale = new Vector3(wallSize, wallHeight, .1f);

        grid = new int [width, heigth];
        gridObjectsV = new GameObject [width + 1, heigth + 1];
        gridObjectsH = new GameObject [width + 1, heigth + 1];
        drawFullGrid();

        GameObject.Find("Ground").transform.localScale =
            new Vector3((width + 1) * wallSize, 1, (heigth + 1) * wallSize);

        GameObject ceiling = GameObject.Find("Ceiling");
        
        ceiling.transform.localScale =
            new Vector3((width + 1) * wallSize, 1, (heigth + 1) * wallSize);

        ceiling.transform.position = new Vector3(ceiling.transform.position.x,
            wallSize - 1, ceiling.transform.position.z);

        generateMazeBinary();
    }

    void drawFullGrid()
    {
        for (int i = 0; i <= heigth; i++)
        {
            for (int j = 0; j <= width; j++)
            {
                if (i < heigth)
                {
                    float vWallSize = verticalWall.transform.localScale.z;
                    float xOffset, zOffset;

                    xOffset = -(width * vWallSize) / 2;
                    zOffset = -(heigth * vWallSize) / 2;


                    gridObjectsV[j, i] = (Instantiate(verticalWall,
                        new Vector3(-vWallSize / 2 + j * vWallSize + xOffset, wallSize / 2, i * vWallSize + zOffset),
                        Quaternion.identity));

                    gridObjectsV[j, i].active = true;
                }

                if (j < width)
                {
                    float hWallSize = horizontalWall.transform.localScale.x;
                    float xOffset, zOffset;
                    xOffset = -(width * hWallSize) / 2;
                    zOffset = -(heigth * hWallSize) / 2;

                    gridObjectsH[j, i] = (Instantiate(horizontalWall,
                        new Vector3(j * hWallSize + xOffset, wallSize / 2, -(hWallSize / 2) + i * hWallSize + zOffset),
                        Quaternion.identity));

                    gridObjectsH[j, i].active = true;
                }
            }
        }
    }

    void generateMazeBinary()

    {
        for (int row = 0; row < heigth; row++)

        {
            for (int cell = 0; cell < width; cell++)

            {
                float randomNumber = Random.Range(0, 100);

                int carvingDirection;

                if (randomNumber > 30) carvingDirection = N;
                else carvingDirection = E;

                if (cell == width - 1)

                {
                    if (row < heigth - 1) carvingDirection = N;
                    else carvingDirection = W;
                }

                else if (row == heigth - 1)

                {
                    if (cell < width - 1) carvingDirection = E;

                    else carvingDirection = -1;
                }

                grid[cell, row] = carvingDirection;

                // switch (grid[cell, row])
                // {
                //     case N :
                //         grid[cell + 1, row]
                // }
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}