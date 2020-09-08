using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] float _bobSpeed = .02f;
    [SerializeField] float upBound = 3.5f;
    [SerializeField] float downBound = 1f;

    bool goingUp = true;

    void FixedUpdate()
    {
        //bob sphere up and down
        if (goingUp)
        {
            this.transform.position += Vector3.up * _bobSpeed; // (0f, _bobSpeed, 0f);
        }
        else
        {
            this.transform.position -= Vector3.up * _bobSpeed;
        }

        if (this.transform.position.y > upBound)
        {
            goingUp = false;
        }
        if (this.transform.position.y < downBound)
        {
            goingUp = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //tell gameController to add point and then die
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        //if valid:
        if (playerShip != null)
        {
            playerShip.PointGet();
            this.gameObject.SetActive(false);
        }
    }
}
