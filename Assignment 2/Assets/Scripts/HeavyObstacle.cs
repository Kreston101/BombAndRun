using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyObstacle : MonoBehaviour
{
    [HideInInspector] public bool obstacleCleared = false;

    private float xPos;
    private float height;
    //gets the objects xPos and yPos (height)
    void Start()
    {
        xPos = transform.position.x;
        height = transform.position.y;
    }

    void Update()
    {
        //checks the obstacles height
        //the heavy obstacle is considered destroyed when height is <= -8
        if (transform.position.y <= -8 && obstacleCleared == false)
        {
            obstacleCleared = true;
        }
    }

    //if a bomb hits the object
    //small bombs deal 1 damage to heavy objects
    //big bombs deal 2 damage to heavy objects
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallBomb"))
        {
            height -= 1;
            transform.position = new Vector3(xPos, height, 0f);
        }
        else if (other.gameObject.CompareTag("BigBomb"))
        {
            height -= 2;
            transform.position = new Vector3(xPos, height, 0f);
        }
    }
}