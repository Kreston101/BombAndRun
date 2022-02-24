using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    public Rigidbody rb3d;

    //once the clone spawn, nudge it
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        rb3d.AddForce(new Vector3(10f, 0f, 0f) * 20f);
    }
}
