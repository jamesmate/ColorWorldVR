using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    GameObject player;
    bool loner;
    bool inGroup;

    public float speed; // speed of boid
    float rotationSpeed = 4.0f; // turn speed

    public float proximity = 7.0f; // distance until recognised
    public static float speedOffset = 1.0f;
	
	void Start ()
    {
        speed = Random.Range(3.0f, 10.5f);
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (globalFlock.flockOn)
        {
            if (Random.Range(0, 4) < 1)
                ApplyFlock();
        }

        CheckBoundary();
        SoloMove();

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    public static void NewGoal()
    {
        speedOffset = Random.Range(2.0f, 0.8f);
    }

    void CheckBoundary()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) >= 120)
        {
            loner = true;
        }
        else
        {
            loner = false;
            speed = Random.Range(5, 12);
        }

        if (loner)
        {
            this.transform.LookAt(player.transform.position);
            speed *= 1.5f;

        }
    }
        void ApplyFlock()
        {
            GameObject[] members;
            members = globalFlock.allBoids;

            Vector3 vCentre = Vector3.zero;
            Vector3 vAvoid = Vector3.zero;
            Vector3 goalPos = globalFlock.goalPos;

            float fSpeed = 0.0f;

            float dist;

            int groupSize = 0;

            foreach (GameObject member in members) // loop through all boids
            {
                if (member != this.gameObject)
                {
                    // calculate displacement between the boids
                    dist = Vector3.Distance(member.transform.position, this.transform.position);

                    if (dist <= proximity)  // if distance of other boid is within proximity
                    {
                        groupSize++;    //increase group size
                        vCentre += member.transform.position;  //add the other boids position to the group centre
                        
                        if (dist < 3.0f)    // if within a small distance, create avoidance vector to prevent collision
                        {
                            vAvoid = vAvoid + (this.transform.position - member.transform.position);

                        }
                        Flock otherBoid = member.GetComponent<Flock>(); 
                        fSpeed += otherBoid.speed; // add other boids speed to the flock speed

                    }
                }
            }

            if (groupSize > 0) // if boid is in a group
            {
                inGroup = true;
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);    // find average position of group
                speed = fSpeed / groupSize;     // make speed equal average speed of group

                Vector3 direction = (vCentre + vAvoid) - transform.position;    // create direction vector towards center and away from other boids
                if (direction != Vector3.zero)    
                {                                                   // if direction is not straight, slerp towards desired heading
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                }
            }
        }

        void SoloMove()
        {
            if (!inGroup)
            {
                if (Random.Range(0, 1000) < 60)
                {
                    Vector3 playerPos = player.transform.position;
                    Vector3 direction = transform.position - new Vector3(playerPos.x + Random.Range(-180, 180),
                                                    playerPos.y + Random.Range(-45, 45),
                                                    playerPos.z + Random.Range(-180, 180));
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                    speed = Random.Range(3, 10.5f);
                }

                if (transform.position.y < 50)
                {
                Vector3 reverse = player.transform.position + this.transform.position; //turn around
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(reverse), rotationSpeed * Time.deltaTime);
                }
            
            }
        }

}
