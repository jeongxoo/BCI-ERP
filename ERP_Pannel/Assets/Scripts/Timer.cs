using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float time;
    public Text[] text = new Text[4];
    public int n = 0;
    public bool isTimeCheck = false;
 
    private void Update () {
        if(isTimeCheck)
        {
            time += Time.deltaTime;
            timeText.text = time.ToString ();
        }
    }

    public void GoText()
    {
        if (n != 10) {
            text[n].text = timeText.text;
            n++;
        } else {
            Time.timeScale = 0.0f;
        }
    }
}