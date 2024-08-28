using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Scriptable Object/MapData")]
public class MapData : ScriptableObject
{
    [Header("# Main Info")]
    public string MapName;
    public int MapTime;
}
