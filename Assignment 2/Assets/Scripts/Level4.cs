using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    public GameObject tree, pillar, drone, plane, landingPlane;
    public GameObject[] airSpawnPoints, groundSpawnPoints;
    public Text scoreText, level;
    public int planeSpeed = 5;
    public List<GameObject> obstacles;

    private Plane planeScript;
    private DropBomb bombingScript;
    private int score = 0;
    private int levelNumber = 4;
    private Vector3 startPos = new Vector3(-14f, 5f, 0);
    private float playerHeight;
    private bool levelCleared = false;

    void Start()
    {
        //Get scripts to access variables and functions
        planeScript = FindObjectOfType(typeof(Plane)) as Plane;
        bombingScript = FindObjectOfType(typeof(DropBomb)) as DropBomb;

        //loop through the spawn points and spawn in the drones, then add them to the obstacles list
        for (int i = 0; i <= airSpawnPoints.Length - 1; i++)
        {
            obstacles.Add(Instantiate(drone, airSpawnPoints[i].transform.position, drone.transform.rotation));
        }

        //instantiate the other obstacles on the ground
        obstacles.Add(Instantiate(tree, groundSpawnPoints[1].transform.position, drone.transform.rotation));
        obstacles.Add(Instantiate(pillar, groundSpawnPoints[0].transform.position, drone.transform.rotation));

        //send the plane to the start position and get the players initial height
        plane.transform.position = startPos;
        playerHeight = plane.transform.position.y;

        //set the amount of bombs the player gets for the stage
        bombingScript.SetNumBombs(10, 5);

        level.text = $"Level {levelNumber}";

        //3 second delay before the level starts, gives the player time to prepare
        StartCoroutine(GetReady());
    }

    void Update()
    {
        //check is the level has been cleared yet
        if (levelCleared == false)
        {
            //check is there are still obstacles left on the field, calls else if all obstacles are cleared
            if (obstacles.Count != 0)
            {
                //loop through and check the "obstacleCleared" variable
                //if true it removes the object from the obstacles array
                for (int i = 0; i <= obstacles.Count - 1; i++)
                {
                    //finds what script is attached to the obstacle and whether to remove it
                    switch (obstacles[i].name)
                    {
                        case "LightGun":
                            if (obstacles[i].GetComponent<LightAAGun>().obstacleCleared == true)
                            {
                                //remove the obstacle from the List, then break out of the loop
                                obstacles.RemoveAt(i);
                                score += 30;
                                scoreText.text = "Score: " + score.ToString();
                                break;
                            }
                            break;
                        case "HeavyGun":
                            if (obstacles[i].GetComponent<HeavyAAGun>().obstacleCleared == true)
                            {
                                //remove the obstacle from the List, then break out of the loop
                                obstacles.RemoveAt(i);
                                score += 30;
                                scoreText.text = "Score: " + score.ToString();
                                break;
                            }
                            break;
                        case "Drone(Clone)":
                            if (obstacles[i].GetComponent<Drone>().obstacleCleared == true)
                            {
                                //remove the obstacle from the List, then break out of the loop
                                obstacles.RemoveAt(i);
                                score += 10;
                                scoreText.text = "Score: " + score.ToString();
                                break;
                            }
                            break;
                        case "Tree(Clone)":
                            if (obstacles[i].GetComponent<LightObstacle>().obstacleCleared == true)
                            {
                                //remove the obstacle from the List, then break out of the loop
                                obstacles.RemoveAt(i);
                                score += 30;
                                scoreText.text = "Score: " + score.ToString();
                                break;
                            }
                            break;case "Pillar(Clone)":
                            if (obstacles[i].GetComponent<HeavyObstacle>().obstacleCleared == true)
                            {
                                //remove the obstacle from the List, then break out of the loop
                                obstacles.RemoveAt(i);
                                score += 30;
                                scoreText.text = "Score: " + score.ToString();
                                break;
                            }
                            break;
                    }
                }
            }
            else
            {
                //sets level clear and stop the plane from moving
                levelCleared = true;
                score = FinalScore(score, bombingScript.totalBombs);
                scoreText.text = $"Score: {score}";

                //instantiates a landingPlane that will land on the ground, deactivates the plane object
                Instantiate(landingPlane, plane.transform.position, landingPlane.transform.rotation);
                plane.SetActive(false);

                //goes to the next level after a short delay
                Invoke("NextLevel", 5);
            }
        }
    }

    void FixedUpdate()
    {
        //checks if either the level has been cleared, the player crashed or has run out of bombs
        if (levelCleared == false && planeScript.hasCollided == false && bombingScript.outOfBombs == false)
        {
            //moves the plane horizontally until it reaches the end of the screen
            if (plane.transform.position.x <= 14f && playerHeight > -1)
            {
                plane.transform.position += new Vector3(1f, 0f, 0f) * planeSpeed * Time.deltaTime;
            }
            //moves the plane vertically everytime it reaches the end of the screen
            else if (playerHeight > -1)
            {
                playerHeight -= 1;
                plane.transform.position = new Vector3(-14f, playerHeight, 0f);
            }
        }
        else if (planeScript.hasCollided == true)
        {
            //if the player has crashed, reset the level after a small delay
            Invoke("Reset", 2);
        }
    }

    //calculate the final score
    private int FinalScore(int score, int bombsLeft)
    {
        int bombBonus = bombsLeft * 10;
        int finalScore = score + bombBonus;
        return finalScore;
    }

    //reset the level if the player crashed
    private void Reset()
    {
        SceneManager.LoadScene("Level 4", LoadSceneMode.Single);
    }

    //goes to the next level
    private void NextLevel()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    //coroutine to add a delay at the start of the level
    //uses WaitForSecondsRealtime as the timescale is set to zero
    IEnumerator GetReady()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3.0f);
        Time.timeScale = 1;
    }
}
