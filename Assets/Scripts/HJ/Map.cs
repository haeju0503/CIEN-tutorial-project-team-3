using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    public MapData MapData;    

    Text textName;

    public void OnEnable()
    {
        Text texts = GetComponentInChildren<Text>();
        textName = texts;
        textName.text = string.Format("{0}\n플레이시간 : {1}분", MapData.MapName, MapData.MapTime);
    }

    public void OnClick()
    {
        GameManager.instance.maxGameTime = MapData.MapTime * 60;
        //맵데이터 속 적 리스트, 보스 리스트 등을 가지고 pool Manager 자동 생성하면...
 
    }

}
