using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    public GameObject explosionParticle, audioPlayer;
    
    private GameObject explosionClone;

    //if the bomb contacts anything, it explodes, instantiates a particle and then destroys it after a small delay
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        explosionClone = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        audioPlayer = Instantiate(audioPlayer);
        Destroy(explosionClone, 1);
        Destroy(audioPlayer, 1);
    }
}
