using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EU : MonoBehaviour
{
    public EUData data;
    public int level;

    Text textName;
    Text textDesc;
    Text textUpgrade;
    Text textDowngrade;

    private void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        textName = texts[0];
        textDesc = texts[1];
        textUpgrade = texts[2];
        textDowngrade = texts[3];
        textName.text = data.euName;
    }
    private void OnEnable()
    {
        switch (data.euType)
        {
            case EUData.EUType.IntEU:
                textDesc.text = string.Format(data.euDesc, data.intIncrement[level] * 100);
                break;
            case EUData.EUType.FloatEU:
                textDesc.text = string.Format(data.euDesc, data.floatIncrement[level] * 100);
                break;
            case EUData.EUType.SpecialEU:

                break;
            default:
                
                break;
        }
        //작동 X 하나하나하는게 나을지도..
    }
}
