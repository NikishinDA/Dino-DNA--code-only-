using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelObjectType
{
    BearTrap,
    Coin,
    DNA,
    FireWall,
    GatesGreenRed,
    GatesRedGreen,
    Saw,
    Wall,
    Meteorite,
    Enemy
}

public enum ChunkType
{
    Simple,
    Ascent,
    Descent,
    AscentRight,
    AscentLeft,
    PlatformRight,
    PlatformLeft,
    DescentRight,
    DescentLeft,
    Wave
}
[Serializable]
public class LevelObject
{
    public LevelObjectType type;
    public Vector2 position;
}

[Serializable]
public class ChunkTemplate
{
    public LevelObject[] objects;
    public ChunkType chunkType;
}
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelTemplate : ScriptableObject
{
    public ChunkTemplate[] chunks;
}
