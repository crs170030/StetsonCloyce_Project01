using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerShip : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    void MoveShip()
    {
        //Scale by move speed. (S = -1, W = 1, None = 0)
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        //combine direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        //apply movement to the physics object
        _rb.AddForce(moveDirection);
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
}