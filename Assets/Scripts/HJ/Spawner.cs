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
    public float levelTime;
    //전체 게임 시간을 SpawnData의 인자 수로 나눈 값
    //이러면 모든 레벨의 시간이 같아짐
    //레벨마다 시간 다르게 하고싶으면 수정필요

    private int level;
    //스폰레벨, 스폰레벨에 따른 차이점은 spawnData에서 수정

    private float timer;
    //몹 스폰 주기를 계산하기 위한 변수

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }

    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy01>().Init(spawnData[level]);
    }
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