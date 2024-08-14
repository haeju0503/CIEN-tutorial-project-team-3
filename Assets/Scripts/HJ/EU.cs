using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EU : MonoBehaviour
{
    //Decs타입하고 버튼 타입, 이름 타입 각각 따로 만들어보면? 

    public EUData data;
    public int level;

    public enum EUObjectType { name, decs, upButton, downButton}

    public EUObjectType type;

    Text textName;
    Text textDesc;
    Text textUpgrade;
    Text textDowngrade;

    private void Awake()
    {
        LevelChange(0);
    }

    public void LevelChange(int amount)
    {

        if (amount == 0)
        {
            level = 0;
        }
        else if (amount > 0)
        {
            if (level != data.maxLevel)
            {
                level++;
            }
        }
        else if (amount < 0)
        {
            if (level > 0)
            {
                level--;
            }
        }

        Text[] texts = GetComponentsInChildren<Text>();

        switch (type)
        {
            case EUObjectType.name:
                textName = texts[0];
                textName.text = data.euName;
                break;
            case EUObjectType.decs:
                switch (data.euType)
                {
                    case EUData.EUType.IntEU:
                        textDesc = texts[0];
                        int sum = 0;
                        for (int index = 0; index < level; index++)
                        {
                            sum += data.intIncrement[index];
                        }
                        if (level == data.maxLevel)
                            textDesc.text = string.Format("최고레벨입니다!\n현재 최대체력 증가량 : {0}", sum);
                        else
                            textDesc.text = string.Format(data.euDesc, data.intIncrement[level], sum); //이떄만 버튼클릭 소리나게 하고 싶은데...
                        break;
                    case EUData.EUType.FloatEU:
                        
                    case EUData.EUType.SpecialEU:
                        
                    default:

                        break;
                }
                break;
            case EUObjectType.upButton:
                textUpgrade = texts[0];
                if (level == data.maxLevel)
                    textUpgrade.text = string.Format("Lv Max");
                else
                    textUpgrade.text = string.Format("Lv + 1\nCost {0}", data.cost[level]); //이떄만 버튼클릭 소리나게 하고 싶은데...
                break;
            case EUObjectType.downButton:
                textDowngrade = texts[0];
                if (level == 0)
                    textDowngrade.text = string.Format("Lv Min");
                else
                    textDowngrade.text = string.Format("Lv - 1\nCost -{0}", data.cost[level - 1]); //이떄만 버튼클릭 소리나게 하고 싶은데...
                break;
            default:
                break;
        }
    }
}
