using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    private int id;
    private EnemySO enemy;
    [SerializeField] private AvatarController player;
    private int timeSinceLastAttack = 0;


    public int Id { get => id; set => id = value; }

    void FixedUpdate()
    {
        status.moveTo = player.transform.position;
        status.lockMovement = false;
        DoContinuousEffects();
        // if (Vector2.Distance(transform.position, player.transform.position) > enemy.range)
        if (!status.lockMovement)
        {
            Move();
        }
        Attack();
    }

    public void Initialize(EnemySO enemy, Vector3 position, int id)
    {
        this.id = id;
        this.enemy = enemy;
        transform.position = position;
        InitializeEntity();
        //transform.Find("Sprite").localScale = new Vector3(enemy.size, enemy.size, 0);
        healthController.SetMaxHealth(enemy.health);
    }

    public void Move()
    {
        Vector3 target = status.moveTo;
        transform.position = Vector2.MoveTowards(transform.position, target, enemy.speed);
    }

    public void Attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= enemy.range)
        {
            if (timeSinceLastAttack >= enemy.attackInterval)
            {
                timeSinceLastAttack = 0;
                player.AddEffect(enemy.effect);
            }
            else
            {
                ++timeSinceLastAttack;
            }
        }
    }

    public override void HealthDrained()
    {
        Destroy(gameObject);
    }
}
