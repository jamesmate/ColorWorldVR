using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;

    public void Respawn()
    {
        SceneManager.UnloadSceneAsync("DEMO");
        SceneManager.LoadScene("DEMO");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        //Prevent movement while in menu
        player = GameObject.FindWithTag("Player");

        player.GetComponent<FPSController>().enabled = false;
    }

}
