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

    public int[] ArrayIndex1;
    public int[] ArrayIndex2;
    public int[] ArrayIndex3;
    public int[] ArrayIndex4;
    public int[] ArrayIndex5;
    public int[] ArrayIndex6;
    public int[][] ArrayIndex;

    private void Start()
    {
        coroutine = test();
        ArrayIndex1 = new int[] {0, 1, 2};
        ArrayIndex2 = new int[] {3, 4, 5};
        ArrayIndex3 = new int[] {6, 7, 8};
        ArrayIndex4 = new int[] {0, 3, 6};
        ArrayIndex5 = new int[] {1, 4, 7};
        ArrayIndex6 = new int[] {2, 5, 8};
        ArrayIndex = new int[][] {ArrayIndex1, ArrayIndex2, ArrayIndex3, ArrayIndex4, ArrayIndex5, ArrayIndex6};
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
            if(n == 50)
            {
                timer.isTimeCheck = false;
                break;
            }

            int k = SelectRandomNum(-1);
            int j = SelectRandomNum(k);
            int pos = -1;
            

            foreach (int i in ArrayIndex[k]) {
                image[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                foreach (int q in ArrayIndex[j]) {
                    if (q == i) {
                        pos = i;
                    }
                    image[q].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }

                if (pos != -1) {
                    image[pos].color = new Color(0.9f, 0.9f, 0.9f, 0.9f);
                }
            }



            yield return new WaitForSeconds(0.2f);

            foreach (int i in ArrayIndex[k]) {
                image[i].color = new Color(0,0,0,0);
            }

            foreach (int i in ArrayIndex[j]) {
                image[i].color = new Color(0,0,0,0);
            }

            if (pos != -1) {
                image[pos].color = new Color(0,0,0,0);
            }
                            
             

            // image[n % 9].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            // yield return new WaitForSeconds(0.2f);
            // image[n % 9].color = new Color(0,0,0,0);
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

    int SelectRandomNum(int n) {
        int num = 0;
        if (n == -1) {
            num = UnityEngine.Random.Range(0, 6);
        } else {
            switch (n)
            {
                case 0: case 1: case 2:
                    num = UnityEngine.Random.Range(3,6);
                    break;
                
                case 3: case 4: case 5:
                    num = UnityEngine.Random.Range(0,3);
                    break;

                default:
                    break;
            }
        }
        return num;
    }

    [System.Serializable]
    public class RealTime
    {
        public string realTime;
    }
}
