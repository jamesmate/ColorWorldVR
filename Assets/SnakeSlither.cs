using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSlither : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
        for (int i = 1; i < transform.parent.childCount; i++) // loop through segments(children) of snake
        {
            Transform current = transform.parent.GetChild(i);   // get transform of current child in loop
            Transform previous = transform.parent.GetChild(i - 1);  // get transform of previous child in loop

            Vector3 desiredPos = previous.transform.TransformPoint(0, 0, -1.3f); // create a vector slightly behind previous segment
            current.transform.position = Vector3.Lerp(current.transform.position, desiredPos, Time.deltaTime * 9f); // slerp to that position

            Quaternion desiredRot = Quaternion.LookRotation(previous.transform.position - current.position, previous.transform.up); // create rotation towards previous segment
            current.transform.rotation = Quaternion.Slerp(current.transform.rotation, desiredRot, Time.deltaTime * 5f); // slerp to that rotation
            
        }

	}
}
