using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGenerator : MonoBehaviour {

    int nSegments;
    float gap = 0.5f;
    public GameObject headPrefab;
    public GameObject bodyPrefab;

    public static Color segColor;

    Vector3 offset;
    // Use this for initialization
    Color RandomColor()
    {
        return new Color(Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f));
    }

    void Start () {

        // GameObject head = GameObject.Instantiate<GameObject>(headPrefab);
        // head.transform.position = transform.position;
        Color snakeColor = RandomColor();

        nSegments = Random.Range(8, 18);

        for (int i = 0; i < nSegments; i++)
        {
            float segmentSize = 1.6f - (i*0.08f);
            Vector3 temp;

            GameObject prefab = (i == 0) ? headPrefab : bodyPrefab;
            GameObject segment = GameObject.Instantiate<GameObject>(prefab);
            temp = segment.transform.localScale;
            temp.z = segmentSize;
            segment.transform.localScale = temp;

            offset = -Vector3.forward * (1.0f - gap + segmentSize );

            //gameObject.GetComponent<Renderer>().bounds.size.z 
            Vector3 pos = i * offset;
            
            segment.transform.position = transform.TransformPoint(pos);
            segment.transform.rotation = transform.rotation;
            segment.transform.parent = this.transform;


            segColor = snakeColor / nSegments * (nSegments - i);

            segment.GetComponent<Renderer>().material.color = segColor;

            Light lt = segment.GetComponent<Light>();
            lt.color = segColor;

        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
