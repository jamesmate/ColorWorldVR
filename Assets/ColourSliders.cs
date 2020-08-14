using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSliders : MonoBehaviour
{
    public Slider red_terrain, blue_terrain, green_terrain;
    public Slider red_sky, blue_sky, green_sky;
    public Camera skybox;

    public Material terrainMat;
    Color skyboxColor;
   //public Material skyMat;

    // Start is called before the first frame update
    void Start()
    {
        skyboxColor = skybox.backgroundColor;

        red_terrain.value = terrainMat.color.r;
        green_terrain.value = terrainMat.color.g;
        blue_terrain.value = terrainMat.color.b;

        red_sky.value = skyboxColor.r;
        green_sky.value = skyboxColor.g;
        blue_sky.value = skyboxColor.b;

    }

    // Update is called once per frame
    void Update()
    {
        terrainMat.color = new Color(red_terrain.value, green_terrain.value, blue_terrain.value);
        skyboxColor = new Color(red_sky.value, green_sky.value, blue_sky.value);
        skybox.backgroundColor = skyboxColor;
        RenderSettings.fogColor = skyboxColor;
        RenderSettings.fog = true;

        DynamicGI.UpdateEnvironment();
    }
}
