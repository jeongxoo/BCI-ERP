using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Timer : MonoBehaviour
{
    // public GoPannel goPannel;
    public Text timeText;
    public float time;
    public Text[] text = new Text[50];
    public int n = 0;
    public bool isTimeCheck = false;
 

    private void Start() {
    }
    private void Update () {
        // if(isTimeCheck)
        // {
        //     time += Time.deltaTime;
        //     timeText.text = time.ToString ();
        // }
        if(Input.GetKeyDown(KeyCode.Space)) {
            GoText();
        }
    }

    public void GoText()
    {
        if (n != 50) {
            text[0].text = n.ToString();
            // goPannel.realTime.clickedTime[n] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss.fff tt"));
            // goPannel.GoToJson();
            n++;
        } else {
            Time.timeScale = 0.0f;
        }
    }
}