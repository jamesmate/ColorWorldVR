using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI: MonoBehaviour {

    private static Vector3 goalPos = Vector3.zero;

    public const float goalRange = 150.0f;
    private Vector3 goalBound1, goalBound2;
    Vector3 snakePos;
    float dist;

    [SerializeField]
    private float newSpeedAI = 0;
    private static float speedAI;
    public GameObject goalPrefab;


    public static Vector3 Get_goalPos()
    {
        return goalPos;
    }

    public static float Get_speed()
    {
        return speedAI;
    }

    // Use this for initialization
    void Start ()
    {
        speedAI = Random.Range(6.0f, 12.0f);


        snakePos = transform.position;

        goalBound1 = new Vector3(Random.Range(snakePos.x, goalRange),
                                 Random.Range(snakePos.y, goalRange),
                                 Random.Range(snakePos.z, goalRange));

        goalBound2 = new Vector3(Random.Range(snakePos.x, -goalRange),
                                 Random.Range(snakePos.y, -goalRange),
                                 Random.Range(snakePos.z, -goalRange));

        goalPos = new Vector3(Random.Range(goalBound1.x, goalBound2.x),
                                  Random.Range(100, 400),
                                  Random.Range(goalBound1.z, goalBound2.z));

        //Instantiate(goalPrefab, goalPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        snakePos = transform.position;
        dist = Vector3.Distance(snakePos, goalPos);


        if ((Random.Range(0, 1000) < 8.0f) || (dist < 5))
        {
            goalPos = new Vector3(this.transform.position.x + Random.Range(-goalRange, goalRange),
                               this.transform.position.y + Random.Range(-100, 300),
                              this.transform.position.z + Random.Range(-goalRange, goalRange));
            if (goalPos.y < 100)
                goalPos.y = 100;
        
            //goalPrefab.transform.position = goalPos;
            if (speedAI < 0.4f)
                speedAI = 0.4f;
          
            newSpeedAI = speedAI + Random.Range(-0.4f, 0.4f);
            speedAI = newSpeedAI;
        }

        
    }
 }
