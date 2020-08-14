using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class meshGen : MonoBehaviour
{


    Mesh mesh;
    Vector3[] vertices;
    int[] triangle;
    Color32[] colors;
    Color32 triangleColor;

    public int xSize = 20;
    public int zSize = 20;
    public float heightScale = 2.5f;
    public float detailScale = 1.0f;
    // Use this for initialization
    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        ColourTriangles();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z < zSize + 1; z++)
        {
            for (int x = 0; x < xSize + 1; x++)
            {
                float height = ((Mathf.PerlinNoise((x + this.transform.position.x) / detailScale, (z + this.transform.position.z) / detailScale) * heightScale));
                height = Mathf.Pow(height, 6);
                vertices[i] = new Vector3(x * 2.0f, height, z * 2.0f);
                i++;
            }
        }

        triangle = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangle[vert + 0] = tris + 0;
                triangle[vert + 1] = tris + xSize + 1;
                triangle[vert + 2] = tris + 1;
                triangle[vert + 3] = tris + 1;
                triangle[vert + 4] = tris + xSize + 1;
                triangle[vert + 5] = tris + xSize + 2;

                tris++;
                vert += 6;
            }
            tris++;

        }
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 0.9f), Random.Range(0.0f, 1.0f));
    }

    void ColourTriangles()
    {
        colors = new Color32[vertices.Length];
        triangleColor = RandomColor();
        int vert = 0;
        int tri = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x< xSize; x++)
            {
                colors[0 + tri] = triangleColor;
                colors[1 + xSize + tri] = triangleColor;
                colors[1 + tri] = triangleColor;
                colors[2 + xSize + tri] = triangleColor;

                triangleColor = RandomColor();

                tri += 2;
            }
            
        }
        
        //colors[]
        /*for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                colors[0 + tri] = triangleColor;
                colors[1 + xSize + 1 + tri] = triangleColor;
                colors[1 + tri] = triangleColor;

                /*triangleColor = RandomColor();
                colors[1]
                colors[triangle[vert + 3]] = triangleColor;
                colors[triangle[vert + 4]] = triangleColor;
                colors[triangle[vert + 5]] = triangleColor;

        //tris++;
        //tri += 3;
        } */
    } 
    //tris++
 

    void CreateNoise()
    {
        float heightScale = 2.5f;
        float detailScale = 25.0f;
        float sinLength = 0.05f;
        float speed = 2.0f;
        float scale = 12.0f;
        float noiseStrength = 3.0f;
        float noiseWalk = 0.2f;

        mesh = this.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        for (int v = 0; v < vertices.Length; v++)
        {
           float height = ((Mathf.PerlinNoise((vertices[v].x + this.transform.position.x / 2) / detailScale, (vertices[v].z + this.transform.position.z / 2) / detailScale) * heightScale));
            height = Mathf.Pow(height, 6);
            vertices[v].y = height;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangle;
        mesh.colors32 = colors;

        //mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
 
            Gizmos.color = colors[i];
            Gizmos.DrawSphere(vertices[i], .5f);
        }
    }
}

