using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour
{
    float nSegments;
    float segmentSize_z, segmentSize_x, segmentSize_y;
    float gap = 0.5f;
    float totalLength;
    float sizePrevious = 0;
    float randomScale;

    Vector3 offset;

    public GameObject[] headPrefab;
    public GameObject[] bodyPrefab;
    public GameObject[] tailPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Randomize number of segments, in future make it rarer for longer/abnormal length
        nSegments = Random.Range(8.0f, 12.0f);

        //choose which head prefab and resize
        GameObject head = headPrefab[Random.Range(0, headPrefab.Length)];

        //choose which body segment to use and resize
        GameObject body = bodyPrefab[Random.Range(0, bodyPrefab.Length)];

        //choose which body segement to use and resize
        GameObject tail = tailPrefab[Random.Range(0, tailPrefab.Length)];

        float x, y, t;

        randomScale = (Random.Range(0.5f, 5.5f));

        for(int i = 0; i < nSegments; i++)
        {
            //segmentSize = 3.0f * startSize - (i * 0.08f);
            // segmentSize = 3 + (9 * ((2 / nSegments) * i)) - Mathf.Pow(4.9f * ((2 / nSegments) * i), 2);
            t = ((2.0f / nSegments)* i);
            x = 9.0f * t;
           // y = Mathf.Pow(4.9f * ((2.0f / nSegments) * i), 2.0f);
            y = 4.9f * t;
            segmentSize_z = (3.0f + x - (4.9f* Mathf.Pow(t, 2.0f))) * randomScale;
            segmentSize_x = segmentSize_z/2;
            segmentSize_y = segmentSize_x;
   
            Vector3 temp;

            //decide which type of part to add to creature
            GameObject part = (i == 0) ? head : body;
            if (i > nSegments-1) part = tail;
            GameObject segment = GameObject.Instantiate<GameObject>(part);
            
            temp = segment.transform.localScale;
            temp.z = segmentSize_z;
            temp.x = segmentSize_x;
            temp.y = segmentSize_y;
            segment.transform.localScale = temp;

            //Vector3 pos = -segment.transform.parent.forward * segmentSize;
            //segment.transform.position = segment.transform.parent.position;

            offset = -Vector3.forward * (totalLength + segmentSize_z/2);

            //float distance = segment.GetComponentInParent<Collider>().bounds.size.z + gap;
            //Vector3 pos = i * offset;
            Vector3 pos = -offset;
            //offset = -Vector3.forward * distance;
            //Vector3 pos = offset;

            temp = this.transform.position;
            temp.y = temp.y + (80 + (randomScale * 80));
            if (temp.y > 350)
                temp.y = 350;

            segment.transform.position = temp;
            segment.transform.rotation = transform.rotation;
            segment.transform.parent = this.transform;

            totalLength += segmentSize_z + gap;
            sizePrevious = segmentSize_z;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
