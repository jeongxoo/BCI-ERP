using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NewBehaviourScript1 : MonoBehaviour
{
    public Image[] image = new Image[9];
    public int n = 0;
    public float f;
    public Timer timer;
    IEnumerator coroutine;
    public Text currentTime;
    public Image numPanel;
    public Text[] num = new Text[4];
    public RealTime realTime;

    private void Start()
    {
        coroutine = test();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentTime.text = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            coroutine = test();
            timer.isTimeCheck = true;
            StartCoroutine(coroutine);
            realTime.realTime = currentTime.text;
            string jsonData = JsonUtility.ToJson(realTime,true);
            string path = Path.Combine(Application.dataPath,"timeData.json");
            File.WriteAllText(path,jsonData);
        }
    }

    IEnumerator test()
    {
        while(true)
        {
            if(n == 36)
            {
                timer.isTimeCheck = false;
                break;
            }

            image[n % 9].color = new Color(1,1,1,1);
            yield return new WaitForSeconds(0.2f);
            image[n % 9].color = new Color(0,0,0,0);
            n += 1; 
            yield return new WaitForSeconds(f);
        }
    }

    public void Reset()
    {
        foreach(Image m in image)
        {
            m.color = new Color(0,0,0,0);
        }
        currentTime.text = "-";
        n = 0;
        timer.n = 0;
        timer.time = 0;
        timer.timeText.text = "0";
        timer.isTimeCheck = false;
        StopCoroutine(coroutine);
        for(int i = 0; i < 4; i++)
        {
            timer.text[i].text = "-";
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowPanel()
    {
        // Password 5 2 8 3
        numPanel.gameObject.SetActive(true);
    }

    [System.Serializable]
    public class RealTime
    {
        public string realTime;
    }
}
