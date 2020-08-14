using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourLerperSnake : MonoBehaviour
{

    Color startColor, endColor;
    Light lt;
    Material mt;
    GameObject player;
    float t = 0;

    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 0.9f), Random.Range(0.0f, 1.0f));
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        startColor = RandomColor();
        endColor = player.GetComponent<SnakeColour>().snakeColour;

        mt = GetComponent<Renderer>().material;
        lt = GetComponent<Light>();
       /* startColor = GetComponent<Material>().color;
        endColor = SnakeColour.snakeColour;
        lt = GetComponent<Light>();
        mt = GetComponent<Renderer>().material;
        //lt.color = startColor; */
    }

    // Update is called once per frame
    void Update()
    {
        //mt.color = Color.Lerp(startColor, endColor, t);
       // mt.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, t));
        // lt.color = Color.Lerp(startColor, endColor, t);

        mt.color = Color.Lerp(startColor, endColor, t);
        mt.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, t));
        lt.color = Color.Lerp(startColor, endColor, t);

        t += Time.deltaTime / 5;
        if (t > 1.0f)
        {
            t = 0;
            startColor = endColor;
            endColor = player.GetComponent<SnakeColour>().snakeColour;

        }
    }
}