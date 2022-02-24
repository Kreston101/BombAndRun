using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropBomb : MonoBehaviour
{
    public GameObject smallBomb, bigBomb, GunFirePlayer, BombFallingPlayer, audioHolder1, audioHolder2;
    public Button smallBombButton, bigBombButton, fireGunsButton;
    public Text smallBombText, bigBombText;
    [HideInInspector] public int smallBombCount, bigBombCount, totalBombs;
    [HideInInspector] public bool outOfBombs;

    private bool running, cooldown;

    //Adds listeners to the buttons, sets the bools to false;
    void Start()
    {
        smallBombButton.onClick.AddListener(DropSmallBomb);
        bigBombButton.onClick.AddListener(DropBigBomb);
        fireGunsButton.onClick.AddListener(FireGuns);
        running = false;
        cooldown = false;
        outOfBombs = false;
    }

    //check if the player has run out of bombs, sets to true if there are no more bombs
    void Update()
    {
        smallBombText.text = smallBombCount.ToString();
        bigBombText.text = bigBombCount.ToString();
        totalBombs = smallBombCount + bigBombCount;
        if (totalBombs == 0)
        {
            outOfBombs = true;
        }
    }

    //called by LevelManager, sets the amount of bombs for the level and gets the total amount
    public void SetNumBombs(int numSmall, int numBig)
    {
        smallBombCount = numSmall;
        bigBombCount = numBig;
        totalBombs = numSmall + numBig;
    }

    //function added to the small bomb button, starts the coroutine to drop a small bomb
    //sets running to true to start a small delay
    private void DropSmallBomb()
    {
        if (running == false && smallBombCount > 0)
        {
            StartCoroutine(DropSmall());
            smallBombCount -= 1;
            running = true;
        }
    }

    //function added to the big bomb button, starts the coroutine to drop a big bomb
    //sets running to true to start a small delay
    private void DropBigBomb()
    {
        if (running == false && bigBombCount > 0)
        {
            StartCoroutine(DropBig());
            bigBombCount -= 1;
            running = true;
        }
    }

    //function added to the fireGunsButton
    //uses a seperate cooldown
    private void FireGuns()
    {
        if (cooldown == false)
        {
            StartCoroutine(GunFire());
            cooldown = true;
        }
    }

    //coroutines to drop bombs
    //instantiate a bomb below the plane and let it fall
    //waits for a delay between bombs so the player doesnt spam their way to completion
    IEnumerator DropSmall()
    {
        audioHolder1 = Instantiate(BombFallingPlayer);
        Instantiate(smallBomb, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z), Quaternion.Euler(0f, 0f, -90f));
        yield return new WaitForSecondsRealtime(0.4f);
        Destroy(audioHolder1,1);
        running = false;
    }

    IEnumerator DropBig()
    {
        audioHolder1 = Instantiate(BombFallingPlayer);
        Instantiate(bigBomb, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z), transform.localRotation);
        yield return new WaitForSecondsRealtime(0.4f);
        Destroy(audioHolder1,1);
        running = false;
    }

    //coroutine to perform a raycast to the drones layer
    //if the drone is hit, it calls a function on the selected drones script
    //has a slightly longer cool down then then bomb drop
    //raycasts 7 units in front of the player
    IEnumerator GunFire()
    {
        audioHolder2 = Instantiate(GunFirePlayer);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 7, LayerMask.GetMask("Drone")))
        {
            if (hit.collider.CompareTag("Drone"))
            {
                hit.collider.GetComponent<Drone>().ShotDown();
            }
        }
        yield return new WaitForSecondsRealtime(1f);
        Destroy(audioHolder2);
        cooldown = false;
    }
}
