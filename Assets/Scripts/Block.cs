using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float previousTime;
    public Vector3 rotationPoint;
    public float fallTime = 0.8f;
    public static int height = 80;
    public static int width = 10;
    private GameObject spawner;
    private static Transform[,] grid = new Transform[width,height];
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(rotationPoint, 90f);

            if (!ValidMove())
            {

                transform.Rotate(rotationPoint, -90f);
            }
                
        }

        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 :fallTime ) )
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                this.enabled = false;
                spawner.GetComponent<Spawner>().SpawnTetrominoe();

            }
                previousTime = Time.time;
        }

    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {

            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    void CheckForLines()
    {
        for (int i = height-1; i >= 0; i++)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }

        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if(grid[j,i]==null)
            {
                return false;
            }

        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;

        }

    }

    void RowDown(int i)
    {
        for (int y = 0; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j,y] !=null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }

        }
    }

    bool ValidMove()
    {

        foreach (Transform children in transform)
        {

            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >=height)
            {

                return false;
                
            }

            if (grid[roundedX,roundedY] !=null)
            {
                return false;
            }

        }

        return true;
    }

}
