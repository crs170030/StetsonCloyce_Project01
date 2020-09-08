using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpeed : MonoBehaviour
{
    [SerializeField] float speeen = 0.6f;

    // Update is called once per frame
    void Update()
    {
        //rotate
        this.transform.Rotate(0f, speeen, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider with Speed Powerup Entered!");

        PlayerShip player = other.GetComponent<PlayerShip>();
        //PlayerShip player = _player;

        if (player != null)
        {
            Debug.Log("Speeding Up!");
            //call player function which calls to activate shield object and script
            player.SpeedUp(true);

            //make powerup go away
            this.gameObject.SetActive(false);
            //_hitbox.enabled = !_hitbox.enabled;
            //_mesh.enabled = !_mesh.enabled;
        }
    }
}
