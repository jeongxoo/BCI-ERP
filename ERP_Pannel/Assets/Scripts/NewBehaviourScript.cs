using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float f;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator test()
    {
        bool a = false;
        SpriteRenderer spriteRenderer;
        spriteRenderer = image.GetComponent<SpriteRenderer>();
        while(true)
        {
            //yield return null;
            /*
            if(!a)
            {
                image.color = new Color(1,1,1,1);
                //spriteRenderer.color = Color.black;
                a = !a;
            }
            else
            {
                image.color = new Color(0,0,0,0);
                a = !a;
            }
            */
            image.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(0.02f);
            image.color = new Color(0,0,0,0);
            yield return new WaitForSeconds(f);
        }
    }
}
