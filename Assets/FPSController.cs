using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class FPSController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject pauseMenu;
    public float speed = 50.0f;
    public int rotationSpeed;
    public int pitchSpeed;
    public int rollSpeed = 50;

    public bool enablePitch;

    // Use this for initialization
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
    }

    void Roll(float angle)
    {
        transform.Rotate(mainCamera.transform.forward, angle);
    }

    void Move(float value)
    {
        transform.position -= mainCamera.transform.forward * value;
    }

    void Fly(float value)
    {
        transform.position += Vector3.up * value;
    }

    void Strafe(float value)
    {
        transform.position += mainCamera.transform.right * value;
            
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX, mouseY;
        float speed = this.speed;

        //QUIT
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
            

        //MOVEMENT
        float walk = Input.GetAxis("LS_V");
        float strafe = Input.GetAxis("LS_H");
        
        //YAW
        float rotation = Input.GetAxis("RS_H") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        //PITCH
        if (enablePitch)
        {
            float pitch = Input.GetAxis("RS_V") * pitchSpeed;
            pitch *= Time.deltaTime;
            transform.Rotate(pitch, 0, 0);
        }
        //ROLL RESET
        if(Input.GetButtonDown("X Button"))
        {
            Quaternion newRotation = this.transform.localRotation;
            if (newRotation.x < 90) {
                newRotation.z = 0;
            }
            else if (newRotation.x > 90) {
                newRotation.z = 180;
            }
            transform.localRotation = newRotation;
           
        }

        //ROLL
        float rollValue = Input.GetAxis("Trigger");
        Roll(rollValue * -rollSpeed * Time.deltaTime);
       
        //SPRINT
        if(Input.GetButton("L3 Button"))
        {
            speed *= 3.0f;
        }

        if (Input.GetButton("RB"))
        {
            globalFlock.flockCommand = true;
            
        }
        else
            globalFlock.flockCommand = false;

        if (Input.GetButton("LB"))
        {
            globalFlock.flockOn = false;
            Debug.Log("flock off");
        }
        else
            globalFlock.flockOn = true;

        //MENU
        if(Input.GetButton("Start"))
        {
            Debug.Log("START PRESSED");
            pauseMenu.SetActive(true);

            Vector3 menuPosition = this.transform.position;
            menuPosition.y += 10;
            menuPosition += this.transform.forward * 40;

            //pauseMenu = GameObject.FindWithTag("Menu");
            pauseMenu.transform.rotation = this.transform.rotation;
            pauseMenu.transform.position = menuPosition;

        }

        Move(walk * speed * Time.deltaTime);
        Strafe(strafe * speed * Time.deltaTime);
    }
}