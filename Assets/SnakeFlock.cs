using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFlock : MonoBehaviour
{

    public float speed = 10.1f;
    float rotationSpeed = 10.5f;
    public float proximity = 10.0f;
    public static float speedOffset = 1.0f;

    float theta = 0;
    public float angle = 70;
    public float frequency = 1;
    Vector3 axis = new Vector3(0, 1, 1);
    Vector3 bearing;

    void Start()
    {
        speed = Random.Range(1.5f, 3.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.localRotation = Quaternion.AngleAxis(Mathf.Sin(theta) * angle, axis);
        //bearing = transform.localRotation * Vector3.forward;
        //speedOffset = 0.0f;
        if (Random.Range(0, 4) < 1)
            ApplyRules();

        transform.Translate(0, 0, Time.deltaTime * speed);
        

        theta += Time.deltaTime * frequency;
    }

    public static void NewGoal()
    {
        speedOffset = Random.Range(1.1f, 0.9f);
    }

    void ApplyRules()
    {
        GameObject[] members;
        members = GlobalFlock2.allBoids;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;

        Vector3 goalPos = GlobalFlock2.goalPos;

        float fSpeed = 0.0f;

        float dist;

        int groupSize = 0;

        foreach (GameObject go in members)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= proximity)  // if distance of other boid is within proximity
                {
                    groupSize++;
                    vCentre += go.transform.position;  //add the other boids position to the group centre

                    if (dist < 5.0f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);

                    }
                    SnakeFlock otherBoid = go.GetComponent<SnakeFlock>();
                    fSpeed = fSpeed + otherBoid.speed;

                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);
            speed = fSpeed / groupSize;

            Vector3 targetDirection = (vCentre + vAvoid) - transform.position;

            if (targetDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection) , rotationSpeed * Time.deltaTime) ;
            }
        }
    }




}
