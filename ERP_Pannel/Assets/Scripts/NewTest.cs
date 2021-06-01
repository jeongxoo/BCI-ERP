using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTest : MonoBehaviour
{
    int n;
    public Image image;
    public Text text;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Go();
        }
    }

    IEnumerator test()
    {
        while(true)
        {
            n = Random.Range(0,9);
            yield return new WaitForSeconds(0.5f);
            text.text = n.ToString();
            image.color = new Color(0.5f,0.5f,0.5f,0.5f);
            yield return new WaitForSeconds(0.2f);
            image.color = new Color(0,0,0,0);
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void Go()
    {
        coroutine = test();
        StartCoroutine(coroutine);
    }
}
