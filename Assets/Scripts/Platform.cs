using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{

    public GameObject[] obstacles; // 장애물 오브젝트들 배열 선언

    //public GameObject[] Coin;//점수 추가 코인들

    //public GameObject[] Invincible; //무적의 상태

    // 인덱스 2번 
    int index = 2;
    float x = -3.5f;
    float y = 2.5f;

    List<GameObject> list;

    private void Awake()
    {
        list = new List<GameObject>();
        AddList();
    }
    
    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {


        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].activeSelf == false)
            {
                list[i].SetActive(true);
            }
        }
        

        //for (int i = 0; i < obstacles.Length; i++)
        //{
        //    //if (Random.Range(0, 3) == 0)
        //    //{
        //    //    obstacles[i].SetActive(true);
        //    //}
        //    //else
        //    //    obstacles[i].SetActive(false);

        //    //상위 주석 처리한 코드를 한줄 처리하기
        //    obstacles[i].SetActive(Random.Range(0, 3) == 0);
        //}


        //for (int y = 0; y < Coin.Length; y++)
        //{
        //    Coin[y].SetActive(true);
        //}

        //for (int x = 0; x < Invincible.Length; x++)
        //{
        //    Invincible[x].SetActive(Random.Range(0 , 12) == 0);
        //}   
        
    }

    public void AddList()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 10) < 7)
            {
                index = 2;
            }
            else
            {
                index = 3;
            }

            list.Add(Instantiate(obstacles[index], new Vector2(x, y), Quaternion.identity));

            list[i].SetActive(true);
            list[i].transform.SetParent(this.gameObject.transform, false);
            x += 1;

        }
    }
}