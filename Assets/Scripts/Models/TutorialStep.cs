using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class TutorialStep
{
    public Transform transform;
    public string title;
    [TextArea(3,10)]
    public string content;
    public MoveDirection placement;
    public bool completeOnPlace;
    private bool completed = false;
    private bool isCurrent = false;
    public bool IsCurrent { get => isCurrent; set => isCurrent = value; }
    public bool Completed { get => completed; set => completed = value; }
}
