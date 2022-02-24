using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [HideInInspector] public bool obstacleCleared = false;
    public GameObject drone;

    //called by the DropBombs script, sets the obstacle to cleared and destroys it
    public void ShotDown()
    {
        Destroy(drone);
        obstacleCleared = true;
    }
}
