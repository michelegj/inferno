using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private int time = 1;

    [Header("Keybinds")]
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] KeyCode pauseKey = KeyCode.P;


    private void Update()
    {
        Reload();
    }

    void Reload()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else if (Input.GetKeyDown(pauseKey))
        {
            if (time.Equals(0))
            {
                time = 1;
                ResumeGame();
            }
            else if (time.Equals(1))
            {
                time = 0;
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        //0
        Time.timeScale = time;
    }

    void ResumeGame()
        {
            //1
            Time.timeScale = time;
        }

}
