using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class properties : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector2.zero;
        rb.inertiaTensor = new Vector3(25,25,25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
