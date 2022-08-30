using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour 
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    private int score = 0; // 게임 점수

    public Text PowerTimeUI; // 시간 출력 UI

    public float powertime = 0;
    public bool isPowerOn = false; 


    //public bool isUnbeatTime;   //무적상태인지 체크하기

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() 
    {
        
        // 싱글톤 변수 instance가 비어있는가?
        // 이건 반쪽짜리 싱글톤이다!!!!!!!!!!!!!!!!!
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
            // 생성할때 현재 존재함으로 자기 자산에게 할당이 가능하다.
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    public void Update() 
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //게임을 재시작 하는것과 같은거다.
        }

        if (isPowerOn)
        {           
            powertime -= Time.deltaTime;
            Debug.Log(powertime);
            PowerTimeUI.text = "Power Time : " + Mathf.Round(powertime);


            if (powertime <= 0 )
            {
                isPowerOn = false;
                powertime = 0;
            }

        }
    }

    

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore) 
    {
        
        if(isGameover == false)
        {
            //점수 증가
            score += newScore;
            
            scoreText.text = "Score: " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() 
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

    public void AddPowerTime(int newPowertime)
    {
        //Debug.Log("Parameter" + newPowertime);


        
           // Debug.Log("Before Sum" + powertime);
        powertime += newPowertime;
        Debug.Log(powertime);
        //  Debug.Log("Sum" + powertime);
        PowerTimeUI.text = "Power Time : " + Mathf.Round(powertime);

    }


    public void OnPlayerPowerup(bool set)
    {
        isPowerOn = set;  // 무적 상태 시작
        //powertime = 0;  // 무적상태 시간 초기화
    }

}