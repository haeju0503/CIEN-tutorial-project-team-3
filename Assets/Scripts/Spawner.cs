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

    private int level;
    //��������, ���������� ���� �������� spawnData���� ����

    private float timer;
    //�� ���� �ֱ⸦ ����ϱ� ���� ����

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f) == 0.1 ? 99 : 0;
        //���� level�� ������ 0����
        //���߿� �����Ұ�!!
        
        if (timer > spawnData[level].spawnTime)
        {
            //Spawn();
            timer = 0f;
        }
        
    }
    /*
    void Spawn()
    {
        GameObject enemy = GameManager.instance.Ǯ�޴��� ���� �̸�(pool).Ǯ�޴��� ��ũ��Ʈ���� �� �����ϴ� �Լ�(Get(0));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<�� ��ũ��Ʈ(Enemy)>().Init(spawnData[level]);
    }
    */

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