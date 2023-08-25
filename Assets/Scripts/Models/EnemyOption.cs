using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyOption
{
    public EnemySO enemy;
    public int number;
    public int weight;

    public int GetNumber()
    {
        return number * PlayerManager.CurrentLevel;
    }
}
