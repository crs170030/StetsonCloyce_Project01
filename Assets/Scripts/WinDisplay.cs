using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDisplay : MonoBehaviour
{
    public Image _render;

    // Start is called before the first frame update
    void Awake()
    {
        _render = GetComponent<Image>();
        //_render = gameObject;
        _render.enabled = !_render.enabled;
    }

    public void ChangeUI()
    {
        _render.enabled = !_render.enabled;
    }
}
