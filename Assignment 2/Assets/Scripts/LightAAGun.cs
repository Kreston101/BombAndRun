using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAAGun : MonoBehaviour
{
    [HideInInspector] public bool obstacleCleared = false;
    public GameObject targetPoint;

    private float health;
    //gets the objects xPos and yPos (height)
    void Start()
    {
        health = 2;
    }

    //checks the aa guns health
    //is its health is zero it is considered destroyed
    void Update()
    {
        if (health <= 0 && obstacleCleared == false)
        {
            obstacleCleared = true;
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            targetPoint.SetActive(false);
        }
    }

    //if a bomb hits the object
    //small bombs deal 1 damage of health to light aa guns
    //big bombs deal 2 damage to light aa guns, essentially one shotting it
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallBomb"))
        {
            health -= 1;
            foreach (Transform child in transform)
            {
                if (child.name == "Sandbag")
                {
                    child.position += Vector3.down * 2;
                }
            }
        }
        else if (other.gameObject.CompareTag("BigBomb"))
        {
            health -= 2;
        }
    }
}