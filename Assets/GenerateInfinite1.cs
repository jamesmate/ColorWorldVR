using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Chunk1
{
    public GameObject chunk;
    public float creationTime;

    public Chunk1(GameObject c, float ct)
    {
        chunk = c;
        creationTime = ct;
    }
}

public class GenerateInfinite1 : MonoBehaviour {

    public GameObject plane;
    public GameObject player;  // need to know where player is in order to generate more terrain

    int planeSize = 20; // need to know plane size so we can match chunks up together
    int radius = 5; // radius at which new chunks spawn
    public static int xOffset, zOffset;
    Vector3 startPos; //used to keep track of where player is

    Hashtable chunks = new Hashtable();

     void Start()
     {

        xOffset = Random.Range(-100000, 100000);
        zOffset = Random.Range(-100000, 100000);

        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for(int x = -radius; x < radius; x++)
        {
            for (int z = -radius; z < radius; z++)
            {
                Vector3 pos = new Vector3((x * planeSize + startPos.x), 120, (z * planeSize + startPos.z));

                GameObject c = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                string chunkName = "Chunk_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                c.name = chunkName;
                Chunk chunk = new Chunk(c, updateTime);
                chunks.Add(chunkName, chunk);
            }
        }
     }

    void Update()
    {
        
        //determine how far the player has moved since the last update
        int dx = (int)(player.transform.position.x - startPos.x);
        int dz = (int)(player.transform.position.z - startPos.z);

        if(Mathf.Abs(dx) >= planeSize || Mathf.Abs(dz) >= planeSize)
        {
            float updateTime = Time.realtimeSinceStartup;  //used to know which tiles to remove

            // round player position to nearest floorsize

            int playerX = (int)(Mathf.Floor((int)player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor((int)player.transform.position.z / planeSize) * planeSize);

            for(int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3 ((x * planeSize + playerX), 120, (z * planeSize + playerZ));

                    string chunkName = "Chunk_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    // if chunk name doesnt exist, create new chunk and add it to hashtable
                    if(!chunks.ContainsKey(chunkName))
                    {
                        GameObject c = (GameObject)Instantiate(plane, pos, Quaternion.identity);
                        c.name = chunkName;
                        Chunk chunk = new Chunk(c, updateTime);
                        chunks.Add(chunkName, chunk);
                    }
                    else //update chunk timestamp
                    {
                        (chunks[chunkName] as Chunk).creationTime = updateTime;
                    }
                }
            }
            // destroy all chunks that do not match update time
            // then put new chunks and chunks in a new hashtable

            Hashtable newTerrain = new Hashtable();
            foreach(Chunk chunks in chunks.Values)
            {
                if (chunks.creationTime != updateTime)
                {
                    Destroy(chunks.chunk);
                }
                else
                {
                    newTerrain.Add(chunks.chunk.name, chunks);
                }
            }
            //copy over the new contents to update the hashtable
            chunks = newTerrain;

            //update starting pos for next update
            startPos = player.transform.position;
            

        }
    }

}
