using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerShip : MonoBehaviour
{
    ShieldController _shield;

    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    Rigidbody _rb = null;
    ParticleSystem fireLeft = null;
    bool postToggle = true;

    private void Awake()
    {
        _shield = FindObjectOfType<ShieldController>();

        _rb = GetComponent<Rigidbody>();
        fireLeft = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        MoveShip();
        TurnShip();

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

    public void Kill()
    {
        Debug.Log("Player died a clumsy and painful death.");
        this.gameObject.SetActive(false);
    }

    //Fire Graphics
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

    void Reverse(bool blastnt)
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
    public void ShieldUp()
    {
        Debug.Log("Shield On!!");
        _shield.TurnOn(true);
    }
}