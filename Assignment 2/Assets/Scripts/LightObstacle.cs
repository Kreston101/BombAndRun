using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObstacle : MonoBehaviour
{
    [HideInInspector] public bool obstacleCleared = false;

    private float xPos;
    private float height;
    //gets the objects xPos and yPos (height)
    
    //set the xPos and height of the obstacle
    void Start()
    {
        xPos = transform.position.x;
        height = transform.position.y;
    }

    //checks if the obstacle is lower than a certain heigh
    //the obstacle is considered destroyed if its y position is <= -8.75
    void Update()
    {
        if (transform.position.y <= -8.75 && obstacleCleared == false)
        {
            obstacleCleared = true;
        }
    }

    //if a bomb hits the object
    //small bombs deal 2 damage to light objects
    //big bombs deal 4 damage to light objects
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallBomb"))
        {
            height -= 2;
            transform.position = new Vector3(xPos, height, 0f);
        }
        else if (other.gameObject.CompareTag("BigBomb"))
        {
            height -= 4;
            transform.position = new Vector3(xPos, height, 0f);
        }
    }
}