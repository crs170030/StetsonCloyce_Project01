using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ShieldController : MonoBehaviour
{
    PlayerShip _player = null;

    [SerializeField] Transform _objectToFollow = null;
    [SerializeField] float shSize = 7f;

    Vector3 _objectOffset;
    Vector3 scaleChange;

    [SerializeField] float timerLength = 10f;
    //public bool isActive = false;
    private float elasped = 0f;

    public AudioClip ShutUp;
    public AudioClip ShutDown;
    public AudioSource _shieldStart;
    bool playStart = false; bool playEnd = false;

    private void Awake()
    {
        //active = false;
        //create off set between this position and object's position
        _objectOffset = this.transform.position - _objectToFollow.position;
        _player = FindObjectOfType<PlayerShip>();
        _shieldStart = GetComponent<AudioSource>();
        if (_shieldStart == null)
            _shieldStart = gameObject.AddComponent<AudioSource>();
    }

    //shield
    private void Update()
    {
        //Debug.Log("Shield script active");
        //apply offset every frame to repostion shield
        this.transform.position = _objectToFollow.position + _objectOffset;

        if (_player.shieldActive)
        {
            elasped += Time.deltaTime;
            if (elasped >= timerLength)
            {
                Debug.Log(elasped + " seconds has passed. Shield powering down.");
                elasped = 0f;
                //TurnOn(false);
                _player.ShieldUp(false);
            }
        }

        if (playStart)
        {
            //_shieldStart.Play();
            playStart = false;
            _shieldStart.PlayOneShot(ShutUp, 1f);
        }

        if (playEnd)
        {
            playEnd = false;
            _shieldStart.PlayOneShot(ShutDown, 1f);
        }
    }

    public void TurnOn(bool active = false)
    {
        scaleChange = new Vector3(shSize, shSize, shSize);
        

        if (active)
        {
            //isActive = true;
            //make shield large
            Debug.Log("Grow Shields!");
            playStart = true;

            //this.transform.localScale += scaleChange;
            this.transform.localScale = scaleChange;

            elasped = 0f;
        }
        else
        {
            //isActive = false;
            Debug.Log("Shrink Shields!");
            playEnd = true;
            //this.transform.localScale -= scaleChange; //make shield smaller

            scaleChange = new Vector3(0f, 0f, 0f);
            this.transform.localScale = scaleChange;
        }
    }
}
