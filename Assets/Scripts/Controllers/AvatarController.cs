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
        if (!PlayerManager.IsPaused())
        {
            DoContinuousEffects();
        }
    }

    public override void HealthDrained()
    {
        PlayerManager.levelOver = true;
        gameOverController.OpenGameOverScreen();
    }
}
