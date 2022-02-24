using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAAGun : MonoBehaviour
{
    [HideInInspector] public bool obstacleCleared = false;
    public GameObject targetPoint;

    private float health;
    //sets obstacles health
    void Start()
    {
        health = 4;
    }

    //checks the health of the obstacle
    //adjust the look of the object to indicate how much damage it has take
    //obstacle is cleared if health = 0
    void Update()
    {
        switch (health)
        {
            case 1:
                transform.GetChild(1).position += Vector3.down * 3; 
                break;
            case 2:
                transform.GetChild(0).position += Vector3.down * 3;
                break;
        }
        if (health <= 0 && obstacleCleared == false)
        {
            obstacleCleared = true;
            transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
            targetPoint.SetActive(false);
        }
    }

    //if a bomb hits the object
    //small bombs deal 1 damage of health to heavy aa guns
    //big bombs deal 2 damage to heavy aa guns
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallBomb"))
        {
            health -= 1;
        }
        else if (other.gameObject.CompareTag("BigBomb"))
        {
            health -= 2;
        }
    }
}