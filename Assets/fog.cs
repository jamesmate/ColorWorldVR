using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour
{
    public Camera camera;
    public Shader fogShader;
    // Start is called before the first frame update
    void Start()
    {
        camera.RenderWithShader(fogShader, "Opaque");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
