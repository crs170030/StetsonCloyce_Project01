using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    CameraFollow _camera;  //camera reference to stop music

    public AudioClip explode;
    public AudioSource _audioPlayer;
    bool playExplode = false;

    void Awake()
    {
        _camera = FindObjectOfType<CameraFollow>();
        _audioPlayer = GetComponent<AudioSource>();
        if (_audioPlayer == null)
            _audioPlayer = gameObject.AddComponent<AudioSource>();
    }

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

        if (playExplode)
        {
            playExplode = false;
            _audioPlayer.PlayOneShot(explode, 1f);
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        _camera.StopMusic();
        playExplode = true;

        //Add game over text + score
    }
}
