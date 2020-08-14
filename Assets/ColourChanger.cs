using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger: MonoBehaviour
{

    Color startColor;
    Color endColor;
    Color[] colors;
    Vector3[] vertices;
    Mesh mesh;
    float t = 0;
    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 0.9f), Random.Range(0.0f, 1.0f));
    }

    Color triangleColor;
    int numberOfTriangles;

    // Use this for initialization
    void Start()
    {
        startColor = RandomColor();
        endColor = RandomColor();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        triangleColor = RandomColor();
        numberOfTriangles = vertices.Length / 3;

        for (int i = 0; i < vertices.Length; i++)
        {
            
            for (int j = 0; i == numberOfTriangles; j++)
            {
                colors[i] = triangleColor;
                i++;
            }
            triangleColor = RandomColor();
            colors[i] = triangleColor;
            // colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }

        mesh.colors = colors;
    }

    // Update is called once per frame
    void Update()
    {
        if (mesh == null) return;
        //Color triangleColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
       // colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                colors[i] = triangleColor;
                i++;
            }
            
            // colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }

        mesh.colors = colors;

        t += Time.deltaTime / 2;
        if (t > 1.0f)
        {
            t = 0;
            startColor = endColor;
            endColor = RandomColor();

        }
    }
}