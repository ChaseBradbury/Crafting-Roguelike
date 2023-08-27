using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : EntityController
{
    [SerializeField] private GameOverController gameOverController;

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
        gameOverController.OpenGameOverScreen();
    }
}
