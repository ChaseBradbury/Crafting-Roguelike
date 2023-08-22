using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RepeatOptions
{
    [Tooltip("This Effect repeats over time.")]
    public bool repeatable = false;
    [Tooltip("How often this effect is repeated (50 = 1 second).")]
    public int repeatInterval = 0;
    [Tooltip("How many times this effect repeats.")]
    public int repetitionNumber = 0;
}
