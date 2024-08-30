using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Weapon : MonoBehaviour
{
    public int Id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;
    Player player;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (Id)
        {
            case 0: //(삽) 빙빙 도는 것
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1: //원거리
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            case 5:
                break;
            default:
                break;
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage * GameManager.instance.DamageMul + GameManager.instance.StaticDamage;
        this.count += count;

        if (Id == 0)
            Batch();
        
        if (Id == 5)
            SetAi();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        Id = data.itemId;
        damage = data.baseDamage * Character.Damage * GameManager.instance.DamageMul + GameManager.instance.StaticDamage;
        count = data.baseCount + Character.Count + GameManager.instance.additionalCount;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (Id)
        {
            case 0:
                speed = 105 * Character.WeaponSpeed;
                Batch();
                break;
            case 1:
                speed = 0.5f * Character.WeaponRate;
                break;
            case 5:
                SetAi();
                break;
            default:
                break;
        }

        // 무기 추가했더니 에러가 나서 임시방편으로 넣었습니다
        if (data.itemType == ItemData.ItemType.AI)
            return;

        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;



            Vector3 rotVec = Vector3.back * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is infinity Per.

        }
    }

    // 챗GPT 무기 관리하는 함수
    void SetAi()
    {
        Transform bullet;

        bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = transform;

        bullet.localPosition = Vector3.zero;

        Vector3 scaleVec = new Vector3(1.8f+(count/100), 1.8f+(count/100), 0);
        bullet.localScale = scaleVec;

        bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;

        dir = dir.normalized * (6 + GameManager.instance.shotSpeed);

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

}
