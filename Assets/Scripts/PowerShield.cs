using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    [SerializeField] float speeen = 0.6f;
    private CapsuleCollider _hitbox;

    void Awake()
    {
        _hitbox = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        this.transform.Rotate(0f, speeen, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider with Shield Powerup Entered!");

        //Use object search to locate ship instead of scene search
        PlayerShip player = other.GetComponent<PlayerShip>();

        if(player != null)
        {
            Debug.Log("Boosting Shields");
            //call player function which calls to activate shield object and script
            player.ShieldUp(true);

            //make powerup go away
            this.gameObject.SetActive(false);
            _hitbox.enabled = !_hitbox.enabled;
        }
    }
}
