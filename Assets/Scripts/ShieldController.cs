using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////////TODO: 
///Make Powerup go away
///Make Audio Feedback for Powerup
///Make Second Powerup
///Add Death State(with explosion!)
///Make Player Respawn after 2 seconds
///Make Win Condition
///customize game. (Add lazers and rotating planets?)

public class ShieldController : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;
    [SerializeField] float shSize = 7f;

    Vector3 _objectOffset;
    Vector3 scaleChange;
    //bool active = false;

    private void Awake()
    {
        //create off set between this position and object's position
        _objectOffset = this.transform.position - _objectToFollow.position;
    }

    //shield moves last
    private void LateUpdate()
    {
        //Debug.Log("Shield script active");
        //apply offset every frame to repostion shield
        this.transform.position = _objectToFollow.position + _objectOffset;
    }

    public void TurnOn(bool active = false)
    {
        if (active)
        {
            Debug.Log("Grow Shields!");
            scaleChange = new Vector3(shSize, shSize, shSize);

            this.transform.localScale += scaleChange;
        }
        else
        {
            Debug.Log("Remove Shield!");
        }
    }
}
