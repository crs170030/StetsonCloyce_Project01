using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;
    Vector3 _objectOffsetRotate;

    AudioSource _jams;

    private void Awake()
    {
        //create off set between this position and object's position
        _objectOffset = this.transform.position - _objectToFollow.position;
        //try to do the same with rotation
        //_objectOffsetRotate = this.transform.rotation - _objectToFollow.rotation;

        _jams = GetComponent<AudioSource>();
    }

    //camera moves last
    private void LateUpdate()
    {
        //apply offset every frame to repostion camera
        this.transform.position = _objectToFollow.position + _objectOffset;
        //try to do the same with rotation
        //this.transform.rotation = _objectToFollow.rotation + _objectOffset;
    }

    public void StopMusic()
    {
        _jams.Stop();
    }
}
