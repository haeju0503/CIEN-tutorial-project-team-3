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
        GameManager.instance.gameTime = MapData.MapTime;

    }

}
