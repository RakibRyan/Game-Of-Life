using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{


    private static int screen_width = 64; //1024
    private static int screen_height = 48; //768
    public float speed = 0.1f;
    public float timer = 0;

    public GameObject cellPrefab;


    public GameObject[,] grid = new GameObject[screen_width, screen_height];//640,480;//640,480;//640,480;//640,480;

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();

    }

    // Update is called once per frame


    void Update()
    {

        if (timer >= speed)
        {
            timer = 0;
            CountNeighbours();
            NextGen();

        }
        else
        {
            timer += Time.deltaTime;
        }

    }
    void PlaceCells()
    {
        for (int y = 0; y < screen_height; y++)
        {
            for (int x = 0; x < screen_width; x++)
            {
                //Instantiate cell at x, y, with Quaternion.identity as rotation. This way it will rotate with the original rotation of the cell

                GameObject cell = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
                grid[x, y] = cell;

                grid[x, y].gameObject.tag = "cells";//makes it so it doesn't collide with other cells in the grid.
                grid[x, y].GetComponent<Cell>().SetAlive(RandomAliveCell());

            }
        }
    }

    void CountNeighbours()
    {
        for (int y = 0; y < screen_height; y++)
        {
            for (int x = 0; x < screen_width; x++)
            {
                //number of alive neighbours
                int numNeighbours = 0;//number of neighbours. 

                //north

                if (y + 1 < screen_height)
                {
                    if (grid[x, y + 1].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }
                //east

                if (x + 1 < screen_width)
                {
                    if (grid[x + 1, y].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }
                //south
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }

                //west
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }



                //north-west

                if (x - 1 >= 0 && y + 1 < screen_height)
                {
                    if (grid[x - 1, y + 1].GetComponent<Cell>().isAlive)  // to get infinite amazing pattern remove .GetComponent<Cell>().isAlive
                    {
                        numNeighbours++;
                    }
                }
                //north-east

                if (x + 1 < screen_width && y + 1 < screen_height)
                {

                    if (grid[x + 1, y + 1].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }
                //south-west

                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x - 1, y - 1].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }

                //south-east
                if (x + 1 < screen_width && y - 1 >= 0)
                {
                    if (grid[x + 1, y - 1].GetComponent<Cell>().isAlive)
                    {
                        numNeighbours++;
                    }
                }
                grid[x, y].GetComponent<Cell>().neighbours = numNeighbours; //sets the number of living neighbours to the grid cell itself.



            }
        }
    }

    bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand >= 75) { return true; } else return false;


    }

    void NextGen()
    {

        for (int x = 0; x < screen_width; x++)
        {
            for (int y = 0; y < screen_height; y++)
            {

                //----Rules
                // Any live cells with 2/3 neighbours stays alive
                // Any dead cells with 3 neighbours becomes alive.
                // Any live cells with 1/<3 neighbours dies.


                if (grid[x, y].GetComponent<Cell>().isAlive == true)
                {
                    //Alive
                    if (grid[x, y].GetComponent<Cell>().neighbours == 2 || grid[x, y].GetComponent<Cell>().neighbours == 3) //if the cell is alive.
                    {
                        grid[x, y].GetComponent<Cell>().SetAlive(true);
                    }
                    else
                    {
                        grid[x, y].GetComponent<Cell>().SetAlive(false);

                    }
                }
                else
                {
                    //Dead
                    if (grid[x, y].GetComponent<Cell>().neighbours == 3)
                        grid[x, y].GetComponent<Cell>().SetAlive(true); //Any dead cells with 3 neighbours becomes alive.
                }


            }

        }
    }





}




