using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour

{


    public bool isAlive = false; //the cell is alive or not.
    public int neighbours = 0; //the neighbours of the cell.

    public void SetAlive(bool alive) //sets the state of the cell to be alive or not.
    {
        isAlive = alive;
        if (alive) //if the cell is alive.
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
