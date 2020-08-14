using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotator : MonoBehaviour {

    float theta = 0;
    public float angle = 70;
    public float frequency = 2;
    Vector3 axis = new Vector3(0, 1, 1);

	void Start () {
		
	}
	

	void Update () {

        transform.localRotation = Quaternion.AngleAxis(Mathf.Sin(theta) * angle, axis);
       
        
        theta += Time.deltaTime * frequency;
    }
}
