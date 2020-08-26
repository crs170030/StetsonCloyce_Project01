using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonRise : MonoBehaviour
{
    public GameObject balloon;

    // Update is called once per frame
    void Update()
    {
        //balloon.Position += Vector3.up * 5f;
        balloon.transform.Translate(Time.deltaTime * transform.up * 0.5f);
    }
}
