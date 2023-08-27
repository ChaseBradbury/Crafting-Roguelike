using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemyOption
{
    public EnemySO enemy;
    public int number;
    public int additionalPerLevel;

    public int GetNumber(RoomSO room)
    {
        int level = PlayerManager.CurrentLevel - room.minLevel;
        return number + additionalPerLevel * level;
    }
}
