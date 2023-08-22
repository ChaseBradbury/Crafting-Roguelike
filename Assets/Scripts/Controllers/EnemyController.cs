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
        DoContinuousEffects();
        if (Vector3.Distance(transform.position, player.transform.position) > enemy.range)
        {
            Move();
        }
        if (timeSinceLastAttack >= enemy.attackInterval)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
        else
        {
            ++timeSinceLastAttack;
        }
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
        Vector3 target = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, enemy.speed);
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < enemy.range)
        {
            player.AddEffect(enemy.effect);
        }
    }

    public override void HealthDrained()
    {
        Destroy(gameObject);
    }
}
