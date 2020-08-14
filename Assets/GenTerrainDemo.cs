using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine.Jobs;
using UnityEngine;

public class GenTerrainDemo: MonoBehaviour {

    public GameObject[] structures;
    public bool populate_vines, populate_rocks, populate_structures, terrainMovement; 

    float heightScale = 55.1f;
    float detailScale = 15.0f;
    float sinLength = 0.05f;
    float speed = 2.0f;
    float scale = 12.0f;
    float noiseStrength = 3.0f;
    float noiseWalk = 0.2f;
    int xOffset, zOffset;
    private Vector3[] baseHeight;
    private Vector3 vertex;

    Mesh mesh;

	// Use this for initialization
	void Start ()
    {
        vertex = new Vector3();
        xOffset = InfiniteTerrain.xOffset;
        zOffset = InfiniteTerrain.zOffset;
        //loop over mesh and lift up each vertice. taking y position of each vertex and raise z value up depending on perlin noise
        mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        for (int v = 0; v < vertices.Length; v++)
        {
            //float height = ((Mathf.PerlinNoise((vertices[v].x + (this.transform.position.x + xOffset) / 2.5f) / detailScale, (vertices[v].z + (this.transform.position.z + zOffset)/2.5f) / detailScale) * heightScale));
            // + (2 * Mathf.PerlinNoise(((vertices[v].x + this.transform.position.x/2)/8) / detailScale, ((vertices[v].z + this.transform.position.z/2)/8) / detailScale) * heightScale));
            float height = (Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale, (vertices[v].z + this.transform.position.z) / detailScale) * heightScale
                           + 0.5f * (Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale / 4, (vertices[v].z + this.transform.position.z) / detailScale / 4) * heightScale));
                           //+ 0.25f * (Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale/4, (vertices[v].z + this.transform.position.z) / detailScale/4) * heightScale));
            //height = Mathf.Pow(height, 2.3f);
            //height = Mathf.Round(height / 5.0f) * 5.0f;
            //if (height > 80)
                //height = 150;
            vertices[v].y = height;

        }

    
        mesh.vertices = vertices; //reset the mesh to the new generated values
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //this.gameObject.AddComponent<MeshCollider>(); // add mesh collider so is hard surface

        // POPULATE ROCKS
        if (populate_rocks == true)
        {
            float threshold_rock = 30;
            float sample_rock;
            float scale;

            for (int v = 0; v < vertices.Length; v += 2)
            {
                sample_rock = ((Mathf.PerlinNoise((vertices[v].x + (this.transform.position.x + xOffset / 2.5f)) /30, (vertices[v].z + (this.transform.position.z + zOffset) / 2.5f) / 30) * 4));
                sample_rock = Mathf.Pow(sample_rock, 3.2f);

                if (sample_rock > threshold_rock)
                {

                    Vector3 worldPt = transform.TransformPoint(mesh.vertices[v]);
                    var rock = Resources.Load<GameObject>("Decor/lp_rock2");
                    GameObject.Instantiate(rock, worldPt, Random.rotation);
                    scale = Random.Range(0.8f, 2.5f);
                    rock.transform.localScale = new Vector3(scale, scale, scale);
                    v+=2;

                }
            }
        }

        //POPULATE VINES
        if (populate_vines == true)
        {
            float threshold_vines = 3.5f;
            float sample_vines;

            for (int v = 0; v < vertices.Length; v += 11)
            {
                sample_vines = ((Mathf.PerlinNoise((vertices[v].x + (this.transform.position.x + xOffset / 4) / 2) / 10, (vertices[v].z + (this.transform.position.z + zOffset / 4) / 2) / 10) * 4));

                if (sample_vines > threshold_vines)
                {

                    Vector3 worldPt = transform.TransformPoint(mesh.vertices[v]);
                    worldPt.y += 70;
                    var rock = Resources.Load<GameObject>("Decor/Vine 1.0");
                    GameObject.Instantiate(rock, worldPt, Quaternion.identity);
                    v += 3;

                }
            }
        }

        //POPULATE STRUCTURES
        if (populate_structures == true)
        {
            float threshold_structures = 3.45f;
            float sample_structures;

            for (int v = 0; v < vertices.Length; v += 64)
            {
                sample_structures = ((Mathf.PerlinNoise((vertices[v].x + (this.transform.position.x + xOffset / 5) / 2) / 10, (vertices[v].z + (this.transform.position.z + zOffset / 5) / 2) / 10) * 4));

                if (sample_structures > threshold_structures)
                {

                    Vector3 worldPt = transform.TransformPoint(mesh.vertices[v]);
                    worldPt.y += 18;
                    GameObject structure = structures[Random.Range(0, structures.Length)];
                    Vector3 rotationVec;
                    rotationVec.y = Random.Range(0, 360);
                    rotationVec.z = Random.Range(-35, 35);
                    rotationVec.x = Random.Range(-35, 35);
                    Quaternion rotation = Quaternion.Euler(rotationVec);
                    GameObject.Instantiate(structure, worldPt, rotation);
                    scale = Random.Range(1, 6);
                    structure.transform.localScale = new Vector3(scale, scale, scale);
                    v += 64;

                }
            }
        }
    }

    float theta = 0;
    private int interval = 3;

    void Update () {

        // loop over vertices and apply sin wave to adjust height
        if (terrainMovement == true) // || Time.frameCount % interval == 0)
        {

            if (baseHeight == null)
                baseHeight = mesh.vertices;

            Vector3[] vertices = new Vector3[baseHeight.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertex = baseHeight[i];

                vertex.y += Mathf.Sin(sinLength * (Time.time * speed + (baseHeight[i].x + this.transform.position.x / 2.5f) + (baseHeight[i].y + this.transform.position.y/2.5f) + (baseHeight[i].z + this.transform.position.z / 2.5f)) * scale);
                //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + this.transform.position.x/2 + noiseWalk, baseHeight[i].y + this.transform.position.y/2 + Mathf.Sin(Time.time * 0.3f) ) * noiseStrength;
                vertices[i] = vertex;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }
    }  
}
