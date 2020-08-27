using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        //if valid:
        if(playerShip != null)
        {
            //murdertime epic
            playerShip.Kill();
        }
    }
}
