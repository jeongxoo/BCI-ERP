using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ClickCheck();
    }

    private void ClickCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 카메라 좌표로부터 화면상의 좌표를 관통하는 가상의 ray를 생성하여 리턴하는 함수
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // 가상의 선 ray가 충돌체와 충돌하면 true를 리턴하고 hit에 충돌 대상의 정보를 담아주는 함수
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "Cube")
                {
                    player.transform.position = hit.point;
                }
            }
        }
    }
}
