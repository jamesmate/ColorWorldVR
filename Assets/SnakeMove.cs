using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove: MonoBehaviour {

    private Vector3 goalPos = Vector3.zero;
    public static int goalRange = 1000;
    private Vector3 snakePos;
    float dist;
    float theta;
    float frequency = 0.2f;
    float angle_y = 0.85f;
    float angle_z = 0.8f;

    float sinOffset;
    Quaternion slitherAngle_x, slitherAngle_y;
    Vector3 targetRot, heading;

   // public GameObject goalPrefab;
    float speed;
    public float turnSpeed;

    int moveType;


	// Use this for initialization
	void Start ()
    {
        speed = Random.Range(6.0f, 12.0f);
        snakePos = transform.position;

        goalPos = new Vector3(snakePos.x + Random.Range(-goalRange, goalRange),
                              snakePos.y,
                              snakePos.z + Random.Range(-goalRange, goalRange));

        // Randomise move type 
        moveType = Random.Range(1, 3);
        sinOffset = Random.Range(-180, 180);
    }
	
	// Update is called once per frame
	void Update ()
    {
        snakePos = transform.position;
        dist = Vector3.Distance(snakePos, goalPos);

        //Change goal position 
        if ((Random.Range(0, 10000) < 5) || (dist < 5))
        {
            goalPos = new Vector3(snakePos.x + Random.Range(-goalRange, goalRange),
                                  snakePos.y + Random.Range(-80, 80),
                                  snakePos.z + Random.Range(-goalRange, goalRange));
            if (goalPos.y < 100)
                goalPos.y = 100;
            else if (goalPos.y > 350)
                goalPos.y = 350;

            // Change speed every turn
            speed += Random.Range(-0.4f, 0.4f);

            // Clamp Speed
            if (speed < 0.4f)
                speed = 0.4f;
      
        } 

        //slitherAngle = transform.localRotation; 
        
        

        targetRot = slitherAngle_x * Vector3.up;
        

        //transform.localRotation *= slitherAngle;
        if(moveType == 1)
        {
            slitherAngle_y = Quaternion.AngleAxis(Mathf.Sin(theta + sinOffset) * angle_y, Vector3.up);
            transform.localRotation = slitherAngle_y * Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(goalPos - snakePos), turnSpeed * Time.deltaTime);
        }
       
        else if (moveType == 2)
        {
            slitherAngle_x = Quaternion.AngleAxis(Mathf.Sin(theta + sinOffset) * angle_z, Vector3.right);
            transform.localRotation = slitherAngle_x * Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(goalPos - snakePos), turnSpeed * Time.deltaTime);
        }
       
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(goalPos - snakePos), turnSpeed * Time.deltaTime);

        theta += frequency * Time.deltaTime * Mathf.PI * 2.0f;

        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
