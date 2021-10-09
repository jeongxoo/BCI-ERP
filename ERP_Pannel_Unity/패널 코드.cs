using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ERP_1 : MonoBehaviour
{

    //public Text[] n = new Text[4];
    
    // 프로그램 화면에 password를 보여주기 위한 Text 오브젝트
    public Text[] pw = new Text[4];

    // password의 자릿수를 나타내기 위한 변수
    // 0번 부터 시작하여 3번 까지
    public int number = 0;
    
    // 마지막 password까지의 입력 완료를 표시하기 위한 Text 오브젝트
    public Text success;

    public UnityEngine.UI.Image[] numImage = new UnityEngine.UI.Image[4];

    // 첫 번째 레이어 화면
    // 상하좌우 이미지 점멸이 나오는 화면
    public UnityEngine.UI.Image layer1_panel;

    // 두 번째 레이어 화면
    // 좌우 숫자 점멸이 나오는 화면
    public UnityEngine.UI.Image layer2_panel;
    
    // 
    public UnityEngine.UI.Image[] layer2_Image = new UnityEngine.UI.Image[4];
    public Text[] layer2_Text = new Text[2];

    // Test클래스 참조 변수
    public Test test;
    
    // 픽 인덱스와 타임 데이터를 json에 순차적으로 저장해주기 위해 선언한 변수
    int peek = 0;

    // Layer1 코루틴을 저장할 IEnumerator변수
    IEnumerator s1;

    // Layer2 코루틴을 저장할 IEnumerator변수
    IEnumerator s2;

    // 시작 시 알림용 start text
    public Text startText;

    // 초기 비밀번호 세팅용 애들
    public Canvas setPassCanvas;
    public int setPassCnt = 0;
    public int[] setPassword;
    public Text[] setPassText;

    public Sprite[] numbersImage;
    public SpriteRenderer[] upDown;

    private void Awake()
    {
        // 시작 시 프로그램 화면에 보이는 Password Text 초기화
        foreach(Text e in pw) 
        {
            e.text = "0";
        }
    }

    private void Start()
    {
        // Layer1 코루틴 시작
        s1 = Layer1();
        StartCoroutine(s1);

        // 프로그램 시작 시간 기록
        test.c = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss.fff tt"));

        // 점멸한 순간의 픽 타임과 인덱스 배열 초기화
        test.peekIndex = new int[100];
        test.peekTime = new string[100];

        Save();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            s1 = Layer1();
            StartCoroutine(s1);
            test.c = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss.fff tt"));
            Save();
        }

        // P 버튼으로 프로그램 Pause
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        // R 버튼으로 Layer1 코루틴 Reset
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        // 매 프레임마다 json파일을 load하여 데이터를 갱신하고
        // 0번째 password부터 시작하여 0이 아닌 숫자가 들어오면 password를 입력하고 다음 자릿수로 이동
        LoadGameDataFromJson();
        if(test.password[number] != 0 && number != 4)
        {
            pw[number].text = test.password[number].ToString();
            Save();
            number++;
        }

        if(test.isLayer1)
        {
            isLayerUptrue();
        }
        if(test.isLayer2) {
            isLayerDowntrue();
        }
    }

    // 프로그램 종료
    public void Exit()
    {
        Application.Quit();
    }

    // Layer1 코루틴 시작
    IEnumerator Layer1()
    {
        // 1~2 사이의 랜덤 숫자를 저장하기 위한 변수
        int num;

        // 코루틴이 시작하고 3초 후에 패널 점멸 시작
        startText.text = "READY..";
        startText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        // startText.gameObject.SetActive(true);
        startText.text = "START";
        yield return new WaitForSeconds(0.5f);
        startText.gameObject.SetActive(false);

        switch (test.goToNext)
        {
            case 0:
                upDown[0].sprite = numbersImage[0];
                upDown[1].sprite = numbersImage[1];
                break;

            case 1:
                upDown[0].sprite = numbersImage[2];
                upDown[1].sprite = numbersImage[3];
                break;

            case 2:
                upDown[0].sprite = numbersImage[4];
                upDown[1].sprite = numbersImage[5];
                break;

            default:
                break;   
        }

        while(true)
        {
            // 매 반복마다 num 변수에 1~2 사이의 랜덤 숫자 저장
            // 1이면 0번째 (상 패널) 2이면 1번째 (하 패널)

            num = UnityEngine.Random.Range(1,3);
            upDown[num - 1].gameObject.SetActive(true);

            // 패널이 점멸하는 순간의 시간 기록
            test.peekTime[peek] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss.fff tt"));

            // 해당 시간에 어떤 행 혹은 열이 점멸 하였는지 판단하기 위해 num을 저장
            test.peekIndex[peek] = num;
            test.whatIsDepth[peek] = test.goToNext;
            Save();
            peek++;

            // 패널이 띄워지고 0.3초 대기
            yield return new WaitForSeconds(0.3f);

            // 0.3초 후 패널 꺼짐
            upDown[num - 1].gameObject.SetActive(false);

            // 다시 0.3초 후 패널이 띄워짐
            // 어떤 패널의 띄워지고 다음 패널이 보여지기 까지 총 0.6초가 걸림
            yield return new WaitForSeconds(0.3f);
            yield return null;
        }
    }

    // 프로그램 Pause함수
    public void Pause()
    {
        // timeScale이 0일 때 정지화면
        // timeScale이 1이면 정상 속도
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }

    // 프로그램 Reset함수
    // Test클래스의 배열과 각종 변수 초기화
    public void Reset()
    {
        peek = 0;
        setPassCnt = 0;
        test.c = "";
        test.peekTime = new string[100];
        test.peekIndex = new int[100];
        StopCoroutine(s1);
        foreach(SpriteRenderer sr in upDown)
        {
            sr.gameObject.SetActive(false);
        }

        foreach(Text t in pw)
        {
            t.text = "0";
        }

        for(int i = 0; i < 4; i++)
        {
            test.password[i] = 0;
        }

        number = 0;
        Save();
    }

    // 상단 인식후 이미지 변경
    public void GoToLayerUp()
    {
        test.isLayer1 = false;
        upDown[0].gameObject.SetActive(false);
        upDown[1].gameObject.SetActive(false);

        // Layer1 코루틴을 멈추고 Layer2 코루틴 시작
        StopCoroutine(s1);
        s1 = Layer1();
        StartCoroutine(s1);
        Save();
    }
    
    // 하단 인식후 이미지 변경
    public void GoToLayerDown()
    {
        test.isLayer2 = false;
        upDown[0].gameObject.SetActive(false);
        upDown[1].gameObject.SetActive(false);

        // Layer2 코루틴을 멈추고 Layer1 코루틴 시작
        StopCoroutine(s1);
        s1 = Layer1();
        StartCoroutine(s1);
        Save();
    }

    // 프로그램 저장 경로에 Test클래스를 json형식으로 저장
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(test,true);
        string path = Path.Combine(Application.dataPath,"timeData.json");
        File.WriteAllText(path,jsonData);
    }

    // json파일을 불러와 Test클래스의 데이터 갱신
    public void LoadGameDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "timeData.json");
        string jsonData = File.ReadAllText(path);
        test = JsonUtility.FromJson<Test>(jsonData);
    }

    // 상하단 인식 트리거 검사
    public void isOne() {
        test.isLayer1 = true;
        Save();
    }

    public void isTwo() {
        test.isLayer2 = true;
        Save();
    }

    // 상단일시 이미지 변경용
    public void isLayerUptrue()
    {
        Save();
        LoadGameDataFromJson();
        if (test.goToNext == 0 && test.isLayer1) {
            test.isLayer1 = false;
            test.goToNext = 1;
            Save();
        } else if (test.goToNext == 1 && test.isLayer1){
            test.isLayer1 = false;
            test.goToNext = 0;
            test.password[test.i] = 1;
            test.i += 1;
            Save();
        } else if (test.goToNext == 2 && test.isLayer1) {
            test.isLayer1 = false;
            test.goToNext = 0;
            test.password[test.i] = 3;
            test.i += 1;
            Save();
        }
        GoToLayerUp();
        Save();
    }

    // 하단일시 이미지 변경용
    public void isLayerDowntrue()
    {
        LoadGameDataFromJson();
        if (test.goToNext == 0 && test.isLayer2) {
            test.isLayer2 = false;
            test.goToNext = 2;
            Save();
        } else if (test.goToNext == 1 && test.isLayer2) {
            test.isLayer2 = false;
            test.goToNext = 0;
            test.password[test.i] = 2;
            test.i += 1;
            Save();
        } else if (test.goToNext == 1 && test.isLayer2) {
            test.isLayer2 = false;
            test.goToNext = 0;
            test.password[test.i] = 4;
            test.i += 1;
            Save();
        }
        GoToLayerDown();
        Save();
    }
}


// json파일에 저장될 클래스
[System.Serializable]
public class Test
{
    // 프로그램 시간 시간을 저장할 변수
    public string c;

    // 패널이 점멸한 순간의 시간을 저장할 string배열
    // 점멸한 순간의 시간은 문자열 타입
    public string[] peekTime = new string[100];

    // 어떤 행 혹은 열이 점멸했는지 판단하기 위한 int형 배열
    // 1~4 사이의 랜덤 숫자가 들어가는 num이 저장됨
    public int[] peekIndex = new int[100];

    // 비밀번호의 인덱스를 카운트하는 용
    public int i = 0;

    // 패스워드를 저장할 배열
    // 전부 0으로 초기화 후 시작
    public int[] password = new int[4] {0,0,0,0};

    // 상 하단으로 이동하기 위한 bool 변수
    // true가 되면 각 상하단으로 이동
    public bool isLayer1 = false;
    public bool isLayer2 = false;

    // 상하단 패널의 위치값을 나타내는 변수들
    public int goToNext = 0;
    public int[] whatIsDepth = new int[100];

}