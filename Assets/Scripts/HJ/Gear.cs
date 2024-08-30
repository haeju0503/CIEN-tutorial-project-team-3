using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove: //������Ÿ�� �ٲٸ� �̰͵� �ٲ�ߵ�����
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.Id)
            {
                case 0: //(��) ���� ���� ��
                    float speed = 105 * Character.WeaponSpeed + 50 * GameManager.instance.rate;
                    weapon.speed = speed + (speed * rate);
                    break;
                default: // ����Ʈ�� �� ��°�, ���� �������� ���̽� ���� �����ߵ�����..
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate) * (1f - GameManager.instance.rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {   //�⺻���ǵ� 3����?
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
