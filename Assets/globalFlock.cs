using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour {

    public GameObject boidPrefab;
    //public GameObject goalPrefab;
    public Camera playerCamera;

    GameObject player;
    public static int spawnRange = 150;
    Vector3 spawnBoundary1, spawnBoundary2;
    Vector3 lookPos;
    public Color[] colorArray;

    public static bool flockCommand;
    public static bool flockOn;
    
    
    public static int numBoids = 200;
    public static GameObject[] allBoids = new GameObject[numBoids];

    public static Vector3 goalPos;

    public static Color flockColour;
    Color tempColour;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        

        

        spawnBoundary1 = new Vector3(this.transform.position.x + spawnRange,
                                      this.transform.position.y + spawnRange,
                                      this.transform.position.z + spawnRange);
        spawnBoundary2 = new Vector3(this.transform.position.x - spawnRange,
                                      this.transform.position.y - spawnRange,
                                      this.transform.position.z - spawnRange);

        goalPos = player.transform.position;


        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(spawnBoundary1.x, spawnBoundary2.x),
                                      Random.Range(40, 200),
                                      Random.Range(spawnBoundary1.z, spawnBoundary2.z));
            allBoids[i] = (GameObject)Instantiate(boidPrefab, pos, Quaternion.identity);
                
        }

        Colour();
    }
	
	void Update ()
    {
        if (flockCommand == true)
        {
            lookPos =   playerCamera.transform.position 
                        + playerCamera.transform.forward * 30;
            goalPos = lookPos;
        }

        if (Random.Range(0, 1000) < 2.0f)
        {
            goalPos = GameObject.FindWithTag("Player").transform.position;
            Colour();
        }

        else if (Random.Range(0, 1000) < 3.0f)
        {

            Vector3 homePos = this.GetComponent<Transform>().position;

            goalPos = new Vector3(Random.Range(goalPos.x - 80, goalPos.x + 80),
                                      Random.Range(60, 200),
                                      Random.Range(goalPos.z - 80, goalPos.z + 80));
            //goalPrefab.transform.position = goalPos;
            Colour();
            flockColour = colorArray[Random.Range(0, (colorArray.Length))];
            //Flock.NewGoal();
        }

    }

    void Colour()
    {
        flockColour = colorArray[Random.Range(0, colorArray.Length)];
        //flockColour = tempColour;
    }
}
