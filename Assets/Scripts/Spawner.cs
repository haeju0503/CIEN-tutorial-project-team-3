using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //몹 스폰 위치
    public SpawnData[] spawnData;
    //스폰되는 몹의 정보를 저장하는 변수
    //밑에 SpawnData의 클래스 있음

    private int level;
    //스폰레벨, 스폰레벨에 따른 차이점은 spawnData에서 수정

    private float timer;
    //몹 스폰 주기를 계산하기 위한 변수

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f) == 0.1 ? 99 : 0;
        //현재 level은 무조건 0레벨
        //나중에 수정할것!!
        
        if (timer > spawnData[level].spawnTime)
        {
            //Spawn();
            timer = 0f;
        }
        
    }
    /*
    void Spawn()
    {
        GameObject enemy = GameManager.instance.풀메니저 변수 이름(pool).풀메니저 스크립트에서 몹 스폰하는 함수(Get(0));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<몹 스크립트(Enemy)>().Init(spawnData[level]);
    }
    */

}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    //몹의 모습 몹 스크립트 Enemy의 Init에서 사용됨
    public float spawnTime;
    //몹이 스폰하기까지 필요한 시간(초)
    public int health;
    //몹의 체력
    public float speed;
    //몹의 이동속도
}