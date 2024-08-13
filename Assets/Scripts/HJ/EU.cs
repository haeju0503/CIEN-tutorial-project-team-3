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
        level = -1;
        LevelUp();
    }

    public void LevelUp()
    {
        if (level == data.maxLevel)
            return;

        level++;

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

                        break;
                    case EUData.EUType.SpecialEU:

                        break;
                    default:

                        break;
                }
                break;
            case EUObjectType.upButton:

                break;
            case EUObjectType.downButton:

                break;
            default:
                break;
        }
    }
}
