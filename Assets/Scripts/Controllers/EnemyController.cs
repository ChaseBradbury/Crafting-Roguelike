using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    private int id;
    private EnemySO enemy;
    [SerializeField] private AvatarController player;
    private EnemyManager enemyManager;
    private int timeSinceLastAttack = 0;


    public int Id { get => id; set => id = value; }

    void FixedUpdate()
    {
        if (!PlayerManager.levelOver)
        {
            status.moveTo = player.transform.position;
            status.lockMovement = false;
            status.speedModifier = 1;
            DoContinuousEffects();
            // if (Vector2.Distance(transform.position, player.transform.position) > enemy.range)
            if (!status.lockMovement)
            {
                Move();
            }
            Attack();
        }
    }

    public void Initialize(EnemySO enemy, Vector3 position, int id, EnemyManager enemyManager)
    {
        this.id = id;
        this.enemy = enemy;
        transform.position = position;
        this.enemyManager = enemyManager;
        InitializeEntity();
        Transform sprite = transform.Find("Sprite");
        sprite.localScale = new Vector3(enemy.size, enemy.size, 0);
        sprite.GetComponent<SpriteRenderer>().sprite = enemy.sprite;
        healthController.SetMaxHealth(enemy.health);
    }

    public void Move()
    {
        if (status.moveTo != (Vector2)player.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, status.moveTo, enemy.speed * status.speedModifier);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > enemy.range)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.speed * status.speedModifier);
        }
    }

    public void Attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= enemy.range)
        {
            if (timeSinceLastAttack >= enemy.attackInterval)
            {
                timeSinceLastAttack = 0;
                player.AddEffect(enemy.effect, 1, transform.position);
            }
            else
            {
                ++timeSinceLastAttack;
            }
        }
    }

    public override void HealthDrained()
    {
        enemyManager.EnemyKilled(id);
        Destroy(gameObject);
    }
}
