using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text timeText;

    public void ChangeUI(int time)
    {
        timeText.text = "Time : " + time;
    }
}
