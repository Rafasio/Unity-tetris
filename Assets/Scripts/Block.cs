﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float previousTime;
    public Vector3 rotationPoint;
    public float fallTime = 0.8f;
    public static int height = 80;
    public static int width = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!validMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!validMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(rotationPoint, 90f);
            Debug.Log("dziala");
            if (!validMove())
            {
                Debug.Log("nie dziala");
                transform.Rotate(rotationPoint, -90f);
            }
                
        }

        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 :fallTime ) )
        {
            transform.position += new Vector3(0, -1, 0);
            if (!validMove())
            {
                transform.position -= new Vector3(0, -1, 0);
            }
                previousTime = Time.time;
        }

    }

    bool validMove()
    {

        foreach (Transform children in transform)
        {

            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >=height)
            {

                return false;
                
            }
        }

        return true;
    }

}
