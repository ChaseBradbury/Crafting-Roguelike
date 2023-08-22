using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : EntityController
{
    void Start()
    {
        InitializeEntity();
    }

    void FixedUpdate()
    {
        DoContinuousEffects();
    }

    public override void HealthDrained()
    {
        Debug.Log("DEAD!");
    }
}
