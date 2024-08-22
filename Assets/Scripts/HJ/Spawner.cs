using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //�� ���� ��ġ
    public SpawnData[] spawnData;
    //�����Ǵ� ���� ������ �����ϴ� ����
    //�ؿ� SpawnData�� Ŭ���� ����
    public float levelTime;
    //��ü ���� �ð��� SpawnData�� ���� ���� ���� ��
    //�̷��� ��� ������ �ð��� ������
    //�������� �ð� �ٸ��� �ϰ������� �����ʿ�

    private int level;
    //��������, ���������� ���� �������� spawnData���� ����

    private float timer;
    //�� ���� �ֱ⸦ ����ϱ� ���� ����

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

        if (GameManager.instance.bossState == 1)
        {
            SpawnBoss0();
            GameManager.instance.bossState = 2;
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy01>().Init(spawnData[level]);
    }

    void SpawnBoss0()
    {
        GameObject boss = GameManager.instance.pool.Get(4);
        boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        boss.GetComponent<Boss>().Init();
    }
}
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    //���� ��� �� ��ũ��Ʈ Enemy�� Init���� ����
    public float spawnTime;
    //���� �����ϱ���� �ʿ��� �ð�(��)
    public int health;
    //���� ü��
    public float speed;
    //���� �̵��ӵ�
}