using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        //detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        //if valid:
        if (playerShip != null)
        {
            //murdertime epic (false since not environment)
            playerShip.Kill(false);
        }
    }
}
