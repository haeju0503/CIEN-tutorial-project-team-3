using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EU : MonoBehaviour
{
    //Decs타입하고 버튼 타입, 이름 타입 각각 따로 만들어보면? 

    public EUData data;
    public int level;

    public enum EUObjectType { name, decs, upButton, downButton, scholarship}

    public EUObjectType type;

    Text textName;
    Text textNDesc;
    Text textNLv;
    Text textDesc;
    Text textUpgrade;
    Text textDowngrade;
    Text textScholarship;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Show()
    {
        rect.localScale = Vector3.one;
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    private void Start()
    {
        if (data != null)
            LevelChange(0);
    }

    public void LevelChange(int amount)
    {
        if (amount == 0)
        {
            switch (data.index)
            {
                case "EUHealthLv": //health
                    for (int i = 0; i < PlayerPrefs.GetInt("EUHealthLv"); i++)
                    {
                        if (type == EUObjectType.decs)
                        {
                            level++;
                            StatChange(true);
                        }
                        level = PlayerPrefs.GetInt(data.index);
                    }
                    break;
                case "EUCountLv":
                    for (int i = 0; i < PlayerPrefs.GetInt("EUCountLv"); i++)
                    {
                        if (type == EUObjectType.decs)
                        {
                            level++;
                            StatChange(true);
                        }
                        level = PlayerPrefs.GetInt(data.index);
                    }
                    break;
                case "EUDamageMulLv":
                    for (int i = 0; i < PlayerPrefs.GetInt("EUDamageMulLv"); i++)
                    {
                        if (type == EUObjectType.decs)
                        {
                            level++;
                            StatChange(true);
                        }
                        level = PlayerPrefs.GetInt(data.index);
                    }
                    break;
                case "EUStaticDamageLv":
                    for (int i = 0; i < PlayerPrefs.GetInt("EUStaticDamageLv"); i++)
                    {
                        if (type == EUObjectType.decs)
                        {
                            level++;
                            StatChange(true);
                        }
                        level = PlayerPrefs.GetInt(data.index);
                    }
                    break;
                default:

                    break;



            }
        }
        else if (amount > 0)
        {
            if (level != data.maxLevel)
            {
                if (GameManager.instance.scholarship >= data.cost[level])
                {
                    if (type == EUObjectType.decs)
                    {
                        level++;
                        GameManager.instance.scholarship -= data.cost[level-1];
                        PlayerPrefs.SetInt("Scholarship", GameManager.instance.scholarship);
                        StatChange(true);
                    }
                }
                level = PlayerPrefs.GetInt(data.index);
            }
        }
        else if (amount < 0)
        {
            if (level > 0)
            {
                if (type == EUObjectType.decs)
                {
                    level--;
                    GameManager.instance.scholarship += data.cost[level];
                    PlayerPrefs.SetInt("Scholarship", GameManager.instance.scholarship);
                    StatChange(false);
                }
                level = PlayerPrefs.GetInt(data.index);
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
                    textNDesc.text = string.Format(data.euNDesc, sumf);
                }
                else if (data.euType == EUData.EUType.FloatMulEU)
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
                            textDesc.text = string.Format(data.euuMaxLvDesc, sumf);
                        else
                            textDesc.text = string.Format(data.euDesc, data.floatIncrement[level], sumf); //이떄만 버튼클릭 소리나게 하고 싶은데...
                        break;
                    case EUData.EUType.FloatMulEU:
                        textDesc = texts[0];
                        sumf = 0f;
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
            case "EUHealthLv": // 0 => health

                if (isUp == true)
                {
                    GameManager.instance.AddMaxHealth(data.intIncrement[level - 1]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddMaxHealth(-1 * data.intIncrement[level]);
                }
                PlayerPrefs.SetInt("EUHealthLv", level);

                break;
            case "EUCountLv": // 1 => Count
                if (isUp == true)
                {
                    GameManager.instance.AddCount(data.intIncrement[level - 1]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddCount(-1 * data.intIncrement[level]);
                }
                PlayerPrefs.SetInt("EUCountLv", level);
                break;
            case "EUDamageMulLv": // 2 => Damage Multuple
                if (isUp == true)
                {
                    GameManager.instance.AddDamageMul(data.floatIncrement[level - 1]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddDamageMul(-1 * data.floatIncrement[level]);
                }
                PlayerPrefs.SetInt("EUDamageMulLv", level);
                break;
            case "EUStaticDamageLv":
                if (isUp == true)
                {
                    GameManager.instance.AddDamageMul(data.floatIncrement[level - 1]);
                }
                else if (isUp == false)
                {
                    GameManager.instance.AddDamageMul(-1 * data.floatIncrement[level]);
                }
                PlayerPrefs.SetInt("EUStaticDamageLv", level);
                break;

            default:
                break;
        }



    }
}
