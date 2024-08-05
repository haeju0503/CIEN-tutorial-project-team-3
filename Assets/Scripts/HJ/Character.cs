using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //이서 다 예비, 캐릭터 추가(또는 기획)하면 그걸로 바꾸셈
    public static float Speed
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f; }
    }
    public static float WeaponSpeed
    {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
    }
    public static float WeaponRate
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }
    }
    public static float Damage
    {
        get { return GameManager.instance.playerId == 0 ? 1.2f : 1f; }
    }
    public static int Count
    {
        get { return GameManager.instance.playerId == 2 ? 1 : 0; }
    }
}
