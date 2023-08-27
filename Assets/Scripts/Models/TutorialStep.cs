using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialStep
{
    public SlotController<ItemSO> controller;
    public string title;
    public string content;
    public bool completeOnPlace;
    private bool completed = false;
    private bool isCurrent = false;
    public bool IsCurrent { get => isCurrent; set => isCurrent = value; }
    public bool Completed { get => completed; set => completed = value; }
}
