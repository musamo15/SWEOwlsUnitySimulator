using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public void ButtonClick()
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    
    public void Resume()
    {
        //GameObject.FindWithTag("Pause").SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        //GameObject.FindWithTag("Pause").SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}


