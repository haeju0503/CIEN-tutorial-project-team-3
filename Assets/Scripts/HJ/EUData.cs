using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EU", menuName = "Scriptable Object/EUData")]
public class EUData : ScriptableObject
{
    public enum EUType { IntEU, FloatEU, FloatMulEU, SpecialEU }

    [Header("# Main Info")]
    public EUType euType;
    public string index;
    public string euName;
    [TextArea]
    public string euNDesc;
    [TextArea]
    public string euDesc;
    [TextArea]
    public string euuMaxLvDesc;


    [Header("# Level Data")]
    public int maxLevel;
    public float[] floatIncrement;
    public int[] intIncrement;
    public int[] cost;
}
