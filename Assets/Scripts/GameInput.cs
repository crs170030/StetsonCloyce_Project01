using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Backspace: Restart Level
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Cover me, Reloading level!");
            ReloadLevel();
        }

        /////ESC: Exit the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape was pressed");
            Application.Quit();
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
