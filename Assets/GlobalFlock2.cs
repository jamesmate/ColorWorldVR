using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock2 : MonoBehaviour
{

    public GameObject boidPrefab;
    public GameObject goalPrefab;
    public static int spawnRange = 10;
    Vector3 spawnBoundary1, spawnBoundary2;

    public static int numBoids = 5;
    public static GameObject[] allBoids = new GameObject[numBoids];

    public static Vector3 goalPos = Vector3.zero;

    public static Color flockColour;

    void Start()
    {
        Colour();

        spawnBoundary1 = new Vector3(this.transform.position.x + spawnRange,
                                      this.transform.position.y + spawnRange,
                                      this.transform.position.z + spawnRange);
        spawnBoundary2 = new Vector3(this.transform.position.x - spawnRange,
                                      this.transform.position.y - spawnRange,
                                      this.transform.position.z - spawnRange);


        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(spawnBoundary1.x, spawnBoundary2.x),
                                      Random.Range(spawnBoundary1.y, spawnBoundary2.y),
                                      Random.Range(spawnBoundary1.z, spawnBoundary2.z));
            allBoids[i] = (GameObject)Instantiate(boidPrefab, pos, Quaternion.identity);

        }
    }

    void Update()
    {
        if (Random.Range(0, 1000) < 5.0f)
        {
            goalPos = new Vector3(Random.Range(spawnBoundary1.x, spawnBoundary2.x),
                                      Random.Range(spawnBoundary1.y, spawnBoundary2.y),
                                      Random.Range(spawnBoundary1.z, spawnBoundary2.z));
            goalPrefab.transform.position = goalPos;
            Colour();
            //Flock.NewGoal();
        }
    }

    public static void Colour()
    {
        flockColour = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        //flockColour = flockColour * Mathf.LinearToGammaSpace(intensity);
    }
}
