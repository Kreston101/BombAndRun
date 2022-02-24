using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [HideInInspector] public bool hasCollided = false;
    private Rigidbody rb3d;

    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
    }

    //checks if the plane has collided with an obstacle or drone object
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Drone"))
        {
            hasCollided = true;
            rb3d.AddForce(new Vector3(0f, 10f, 0f) * 20f);
            rb3d.useGravity = true;
        }
    }

    //checks if the plane has touched an AA point
    //this was needed to ensure that bombs could pass through aa points
    //but the plane would still collide with aa points
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hasCollided = true;
            rb3d.AddForce(new Vector3(0f, 10f, 0f) * 20f);
            rb3d.useGravity = true;
        }
    }
}
