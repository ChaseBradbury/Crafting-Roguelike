using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySO enemy;
    private HealthController healthController;

    public void Initialize(EnemySO enemy, Vector3 position)
    {
        this.enemy = enemy;
        transform.position = position;
        //transform.Find("Sprite").localScale = new Vector3(enemy.size, enemy.size, 0);
        healthController = transform.GetComponent<HealthController>();
        healthController.SetMaxHealth(enemy.health);
    }

    public void AddEffect(int test)
    {
        healthController.Damage(test);
    }
}
