using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    CameraFollow _camera;  //camera reference to stop music
    PlayerShip _player;
    ScoreDisplay _score;
    TimeDisplay _time;
    WinDisplay _win;
    Exploder _exp;

    public AudioSource _audioPlayer;
    public AudioClip Explode;
    public AudioClip SpeedBoost;
    public AudioClip SpeedDown;
    public AudioClip Collect;
    public AudioClip TimeOut;
    public AudioClip Winning;

    bool playExplode = false;
    public bool playSpeedBoost = false;
    bool startOver = false;

    [SerializeField] float timerLength = 20f;
    public float timer = 0f;
    [SerializeField] float speedTimerLength = 10f;
    public float speedElasped = 0f;
    [SerializeField] float endGameTimer = 4f;
    float resetElasped = 0f;

    public int pointTotal = 0;
    [SerializeField] int winScore = 25000;

    void Awake()
    {
        _player = FindObjectOfType<PlayerShip>();
        _camera = FindObjectOfType<CameraFollow>();
        _score = FindObjectOfType<ScoreDisplay>();
        _time = FindObjectOfType<TimeDisplay>();
        _win = FindObjectOfType<WinDisplay>();
        _exp = FindObjectOfType<Exploder>();

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
            Debug.Log("Cover me, I'm Reloading level!");
            ReloadLevel();
        }

        /////ESC: Exit the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape was pressed");
            Application.Quit();
        }

        /////Audio
        if (playSpeedBoost)
        {
            playSpeedBoost = false;
            _audioPlayer.PlayOneShot(SpeedBoost, 1f);
        }

        if (playExplode)
        {
            playExplode = false;
            _audioPlayer.PlayOneShot(Explode, 1f);
            _exp.Explode();
        }

        ///Speed Power Timer
        if (_player.speedActive)
        {
            speedElasped += Time.deltaTime;
            if (speedElasped >= speedTimerLength)
            {
                Debug.Log(speedElasped + " seconds has passed. Slowing Down.");
                speedElasped = 0f;
                //TurnOn(false);
                _player.SpeedUp(false);
                _audioPlayer.PlayOneShot(SpeedDown, 1f);
            }
        }

        ////Reload Level Timer
        if (startOver)
        {
            resetElasped += Time.deltaTime;
            if (resetElasped >= endGameTimer)
            {
                Debug.Log(resetElasped + " seconds has passed. Restarting timer!");
                resetElasped = 0f;
                ReloadLevel();
            }
        }
        else
        {
            ///Game Timer
            timer += Time.deltaTime;
            //_time.ChangeUI((int)Math.Round(elasped)); //rounds time to nearest whole int
            _time.ChangeUI((int)(timerLength - timer));
            if (timer >= timerLength)
            {
                Debug.Log(timer + " seconds has passed. Game has ended.");
                timer = 0f;
                _audioPlayer.PlayOneShot(TimeOut, 1f);
                GameOver(true);
            }
        }
    }

    public void addScore()
    {
        pointTotal += 100;
        _audioPlayer.PlayOneShot(Collect, 1f);
        _score.ChangeUI(pointTotal);

        if(pointTotal >= winScore)
        {
            chickenDinner();
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void GameOver(bool timeOut = false)
    {
        Debug.Log("Game Over! Final Score is " + pointTotal);
        _camera.StopMusic();

        if (!timeOut)
        {
            playExplode = true;
        }
        else
        {
            _player.canMove = false; //freeze player on timeOut
        }

        //Add game over text + score

        //Respawn Player
        startOver = true;
    }

    void chickenDinner()
    {
        Debug.Log("Your Winner! Final Score was " + pointTotal);
        _win.ChangeUI();
        _camera.StopMusic();
        _audioPlayer.PlayOneShot(Winning, 1f);
        _player.canMove = false; //freeze player on win

        startOver = true;
    }
}
