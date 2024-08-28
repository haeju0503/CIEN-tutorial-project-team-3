using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Data
{
    public int maintainSec;

    public int maxSDColumn;

    public SpawnData[] spawnData;

    public float LCM;

}
/*
[System.Serializable]
public class SpawnLevelArr
{
    public Data[] SpawnLevel;
}*/
public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //public SpawnData[] spawnData;
    //public float levelTime;

    //public SpawnLevelArr[] Map;

    public SpawnLevelData[] inputData;

    public Data[,] SpawnDatas = new Data[2, 5];
    // 2차원 배열 ("구조체를 인자로 가지는 배열"을 인자로 가지는 배열), 행은 맵 종류, 열이 스폰 레벨
    // 한 스폰레벨 안에 동시에 여러 몹을 스폰하기 위해 만듦

    private int level = 0;

    private float levelTimer = 0;

    private float timer = 0;

    private float[] counter = { 0f, 0f, 0f, 0f, 0f }; //임시방편, maxSDColumn수가 6이상이면 무조건 오류 생김...

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        //levelTime = GameManager.instance.maxGameTime / spawnData.Length;
        /*      최소공배수 구하는 부분1
        for(int index=0; index < 2; index++)
        {
            SpawnDatas[GameManager.instance.MapIndex, index].LCM = GetLCM(SpawnDatas[GameManager.instance.MapIndex, index].spawnData, SpawnDatas[GameManager.instance.MapIndex, index].maxSDColumn);
        }
        */
        for(int index=0; index < 3; index++) //임시, SpawnData 더 만들면 늘리셈 index < 3 에서 3을
        {
            SpawnDatas[0, index].maintainSec = inputData[index].maintainSec;
            SpawnDatas[0, index].maxSDColumn = inputData[index].maxSDColumn;
            SpawnDatas[0, index].spawnData = inputData[index].spawnData;
            SpawnDatas[0, index].LCM = inputData[index].LCM;
        }
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        
        if (levelTimer >= SpawnDatas[GameManager.instance.MapIndex, level].maintainSec)
        {
            

            if (SpawnDatas[GameManager.instance.MapIndex, level].maintainSec != -10) //막레벨은 레벨유지시간 -10으로
            {
                level++;
                levelTimer = 0;
            }
        }

        /*
        if (timer > SpawnDatas[GameManager.instance.MapIndex, level].spawnData[0].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
        */

        if (GameManager.instance.bossState == 1)
        {
            SpawnBoss0();
            GameManager.instance.bossState = 2;
        }

        timer += Time.deltaTime;

        for(int index=0; index < SpawnDatas[GameManager.instance.MapIndex, level].maxSDColumn;index++)
        {

            if (timer - counter[index] >= SpawnDatas[GameManager.instance.MapIndex, level].spawnData[index].spawnTime)
            {
                counter[index] += SpawnDatas[GameManager.instance.MapIndex, level].spawnData[index].spawnTime;
                Spawn(index);
            }           
        }
        if (timer >= SpawnDatas[GameManager.instance.MapIndex, level].LCM)
        {
            timer = 0f;
            for(int index=0; index < 5; index++)
                counter[index] = 0f;
        }


    }
    void Spawn(int index)
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy01>().Init(SpawnDatas[GameManager.instance.MapIndex, level].spawnData[index]);
        
    }

    void SpawnBoss0()
    {
        GameObject boss = GameManager.instance.pool.Get(4);
        boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        boss.GetComponent<Boss>().Init();
    }
    /* 최소공배수 구하는 부분2
    private float GetLCM(SpawnData[] spawnData, int maxColumn)
    {
        if (maxColumn == 1) //몹이 한 마리밖에 없다면 그 몹의 spawnTime이 최대공약수
            return spawnData[0].spawnTime;
        else
        {
            float GCD = spawnData[0].spawnTime;
            int index = 0;

            do
            {
                GCD = GetGCD(GCD, spawnData[index+1].spawnTime);
                index++;
            }
            while (index < maxColumn);

            float Mul = 1f;

            for (index=0; index < maxColumn;)
            {
                Mul *= spawnData[index].spawnTime;
                index++;
            }

            return Mul / GCD;
        }
    }
    private float GetGCD(float A, float B)
    {
        float GCD = 1f;

        if( A < B)
        {
            GetGCD(B, A);
        }
        else
        {
            if(A % B == 0)
            {
                GCD = B;
            }
            else
            {
                GetGCD(B, A % B);
            }
        }
        return GCD;
    }
    */
}
[System.Serializable]
public class SpawnData
{
    public int index;

    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;

    public float damage;
    public int exp;

}