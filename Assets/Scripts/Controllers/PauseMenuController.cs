using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        PlayerManager.paused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        PlayerManager.paused = false;
    }

    public void EndRun()
    {
        PlayerManager.LoseLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
