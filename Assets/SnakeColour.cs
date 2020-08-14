using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeColour : MonoBehaviour {

    public Color snakeColour;

	// Use this for initialization
	void Start () {

        snakeColour = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Random.Range(0, 1000) < 3)
        {
            snakeColour = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        }

    }
}
