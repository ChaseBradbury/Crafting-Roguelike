using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySO enemy;
    private int health;

    public void Initialize(EnemySO enemy, Vector3 position)
    {
        this.enemy = enemy;
        transform.position = position;
        health = enemy.health;
    }
}
