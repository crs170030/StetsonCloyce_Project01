using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

///////////////////TODO: 
///V    Make Powerup go away     
///V    Add powerup functionality
///V    Make Audio Feedback for Powerup
///Make Second Powerup (Double Speed?)
///V    Add Death State
///Make Player Respawn after 2 seconds
///Make Win Condition
///---------------Extra -------------------------
///Make Player Explode upon death
///customize game. (Add lazers and rotating planets?)
///Level Design (place power ups and obstacles)

public class PlayerShip : MonoBehaviour
{
    ShieldController _shield;  //get reference to shield object
    //private BoxCollider _hitbox; //get reference to hitbox component
    private CapsuleCollider _hitbox; //get reference to hitbox component
    //CameraFollow _camera;  //camera reference to stop music
    GameInput _gameController;

    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [SerializeField] float _hitRadius = 2f;
    [SerializeField] float _hitHeight = 6.5f;
    [SerializeField] float _hitRound = 4.5f;

    Rigidbody _rb = null;           //get reference to ship's rigidbody
    ParticleSystem fireLeft = null; //get reference to fire particles
    bool postToggle = true;         //used to toggle fire left and right

    public bool shieldActive = false;

    public AudioClip shieldBump;
    public AudioClip explode;
    public AudioSource  _audioPlayer;
    bool playBump = false;//, playExplode = false;

    private void Awake()
    {
        _shield = FindObjectOfType<ShieldController>();
        _hitbox = GetComponent<CapsuleCollider>();
        _hitbox.height = _hitHeight;
        _hitbox.radius = _hitRadius;
        //_camera = FindObjectOfType<CameraFollow>();
        _gameController = FindObjectOfType<GameInput>();

        _rb = GetComponent<Rigidbody>();
        fireLeft = GetComponent<ParticleSystem>();

        _audioPlayer = GetComponent<AudioSource>();
        if (_audioPlayer == null)
            _audioPlayer = gameObject.AddComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        MoveShip();
        TurnShip();

        //play audio
        if (playBump)
        {
            playBump = false;
            _audioPlayer.PlayOneShot(shieldBump, .5f);
        }
        /*if (playExplode)
        {
            playExplode = false;
            _audioPlayer.PlayOneShot(explode, 1f);
        }*/

        //duplicate fire particle for dual engine action!
        var fireLeftShape = fireLeft.shape;
        if (postToggle)
        {
            fireLeftShape.position = new Vector3(.65f, .5f, -2f);
            postToggle = false;
        }
        else
        {
            fireLeftShape.position = new Vector3(-.65f, .5f, -2f);
            postToggle = true;
        }
    }

    void MoveShip()
    {
        //Scale by move speed. (S = -1, W = 1, None = 0)
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        //combine direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        //apply movement to the physics object
        _rb.AddForce(moveDirection);

        //change fire particle on keypress
        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                Reverse(false);
                BlastEngines(true);
                break;

            case -1:
                BlastEngines(false);
                Reverse(true);
                break;

            default:
                BlastEngines(false);
                Reverse(false);
                break;
        }
    }

    void TurnShip()
    {
        //Scale by turn speed. (A = -1, D = 1, None = 0)
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply our turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        //speeeen the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill(bool environment) //only kill if it hits unfriendly cube
    {
        if (environment)
        {
            if (!shieldActive) //player hit cube with no shield
            {
                Debug.Log("Player died a clumsy and painful death.");
                Death();
            }
            else
            {
                Debug.Log("Player Shield Hit!");
                ShieldUp(false);
            }
        }
        else
        {
            if (!shieldActive) //player hit enemy
            {
                Debug.Log("Player struck by enemy!"); //Add fail fanfare
                Death();
            }
            else
            {
                Debug.Log("Hit enemy!");
                playBump = true;
            }
            
        }
    }

    public void Death()
    {
        //playExplode = true;
        this.gameObject.SetActive(false);
        _gameController.GameOver();
        //_camera.StopMusic();
    }

    //////////Fire Graphics
    void BlastEngines(bool blast)
    {
        var fireLeftShape = fireLeft.shape;
        if (blast)
        {
            //Debug.Log("fire engines");
            //fireLeftShape.radius = 4f;
            fireLeftShape.scale = new Vector3(2.5f, 0.2f, 10f);
        }
        else
        {
            //fireLeftShape.radius = 1f;
            fireLeftShape.scale = new Vector3(0.2f, 0.2f, 10f);
        }
    }

    void Reverse(bool blastnt) //turn particles off if reversing and resume otherwise
    {
        if (blastnt)
        {
            fireLeft.Stop();  
        }
        else
        {
            fireLeft.Play();
        }
    }

    /////////////////////////Powerups
    //Shield
    public void ShieldUp(bool goTime)
    {
        if (goTime)
        {
            Debug.Log("Shield On!!");
            shieldActive = true;

            //turn off ship hitbox
            //_hitbox.enabled = !_hitbox.enabled;
            //Debug.Log("Ship_hitbox.enabled = " + _hitbox.enabled);

            //make hitbox round
            _hitbox.height = _hitRound;
            _hitbox.radius = _hitRound;

            //call ShieldController
            _shield.TurnOn(true);
        }
        else
        {
            shieldActive = false;

            //decrease size of ship hitbox
            _hitbox.height = _hitHeight;
            _hitbox.radius = _hitRadius;

            //call ShieldController
            _shield.TurnOn(false);
        }
    }
}