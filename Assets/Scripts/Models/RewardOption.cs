using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardOption
{
    public ElementSO element;
    public int number;

    public int GetNumber()
    {
        return number;
    }
}
