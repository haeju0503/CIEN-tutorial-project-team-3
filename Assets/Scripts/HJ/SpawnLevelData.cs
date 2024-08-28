using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnLevel", menuName = "Scriptable Object/SpawnLevelData")]
public class SpawnLevelData : ScriptableObject
{
    [Header("# Map Info")]
    public int mapindex;
    public int levelindex;

    [Header("# SpawnData")]
    public int maintainSec;
    public int maxSDColumn;
    public SpawnData[] spawnData;
    public float LCM;




}
