using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EU : MonoBehaviour
{
    //Decs타입하고 버튼 타입, 이름 타입 각각 따로 만들어보면? 

    public EUData data;
    public int level;

    public enum EUObjectType { name, decs, upButton, downButton, scholarship }

    public EUObjectType type;
    public EU levelSaver;

    Text textName;
    Text textNDesc;
    Text textNLv;
    Text textDesc;
    Text textUpgrade;
    Text textDowngrade;
    Text textScholarship;

    private void Start()
    {
        level = 0;
        LevelChange(0);
    }

    public void LevelChange(int amount)
    {
        if (amount == 0)
        {
            
        }
        else if (amount > 0)
        {
            if (level != data.maxLevel)
            {
                if (GameManager.instance.scholarship >= data.cost[level])
                {
                    if (type == EUObjectType.decs)
                    {
                        GameManager.instance.scholarship -= data.cost[level];
                        StatChange(true);
                        level++;
                    }
                }
                level = levelSaver.level;
            }
        }
        else if (amount < 0)
        {
            if (level > 0)
            {
                level--;
                if (type == EUObjectType.decs)
                {
                    GameManager.instance.scholarship += data.cost[level];
                    StatChange(false);
                }
            }
        }

        Text[] texts = GetComponentsInChildren<Text>();

        switch (type)
        {
            case EUObjectType.name:
                textName = texts[0];
                textNDesc = texts[1];
                textNLv = texts[2];
                textName.text = string.Format(data.euName);
                
                if (data.euType == EUData.EUType.FloatEU)
                {
                    float sumf = 0f;
                    for (int index = 0; index < level; index++)
                    {
                        sumf += data.floatIncrement[index];
                    }
                    textNDesc.text = string.Format(data.euNDesc, sumf * 100);
                }
                else if (data.euType == EUData.EUType.IntEU)
                {
                    int sum = 0;
                    for (int index = 0; index < level; index++)
                    {
                        sum += data.intIncrement[index];
                    }
                    textNDesc.text = string.Format(data.euNDesc, sum);
                }
                textNLv.text = string.Format("Lv {0}", level);
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
                            textDesc.text = string.Format(data.euuMaxLvDesc, sum);
                        else
                            textDesc.text = string.Format(data.euDesc, data.intIncrement[level], sum); //이떄만 버튼클릭 소리나게 하고 싶은데...
                        break;
                    case EUData.EUType.FloatEU:
                        textDesc = texts[0];
                        float sumf = 0f;
                        for (int index = 0; index < level; index++)
                        {
                            sumf += data.floatIncrement[index];
                        }
                        if (level == data.maxLevel)
                            textDesc.text = string.Format(data.euuMaxLvDesc, sumf * 100f);
                        else
                            textDesc.text = string.Format(data.euDesc, data.floatIncrement[level] * 100f, sumf * 100f); //이떄만 버튼클릭 소리나게 하고 싶은데...
                        break;
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
            case EUObjectType.scholarship:
                textScholarship = texts[0];
                int scholarship = GameManager.instance.GetScholarship();
                textScholarship.text = string.Format("장학금\n{0}만원", scholarship);
                break;
            default:
                break;
        }
    }

    public void StatChange(bool isUp)
    {
        switch (data.index)
        {
            case 0: // 0 => health
                if (isUp == true)
                {
                    GameManager.instance.AddMaxHealth(data.intIncrement[level]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddMaxHealth(-1 * data.intIncrement[level]);
                }
                break;
            case 1: // 1 => Count
                if (isUp == true)
                {
                    GameManager.instance.AddCount(data.intIncrement[level]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddCount(-1 * data.intIncrement[level]);
                }
                break;
            case 2: // 2 => Damage Multuple
                if (isUp == true)
                {
                    GameManager.instance.AddDamageMul(data.floatIncrement[level]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddDamageMul(-1 * data.floatIncrement[level]);
                }
                break;


            default:
                break;
        }



    }
}
