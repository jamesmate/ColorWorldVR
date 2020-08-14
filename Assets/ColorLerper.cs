using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{

    Color startColor;
    Color endColor;
  
    Light lt;
    float t = 0;
    Color RandomColor()
    {
        
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 0.9f), Random.Range(0.0f, 1.0f));
    }

    // Use this for initialization
    void Start()
    {
        startColor = RandomColor();
        endColor = globalFlock.flockColour;
        lt = GetComponent<Light>();
        //lt.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColor, endColor, t);
        GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, t));
        lt.color = Color.Lerp(startColor, endColor, t);

        t += Time.deltaTime/2;
        if (t > 1.0f)
        {
            t = 0;
            startColor = endColor;
            endColor = globalFlock.flockColour;

        }
    }
}