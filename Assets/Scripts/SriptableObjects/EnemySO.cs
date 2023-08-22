using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [Tooltip("Unique code for the enemy.")]
    public string enemyCode;

    [Tooltip("Name displayed when hovered.")]
    public string enemyName;

    [Tooltip("How much damage the enemy can take.")]
    public int health;

    [Tooltip("How large the hitbox is.")]
    public float size;

    [Tooltip("How fast the enemy moves.")]
    public float speed;

    [Tooltip("How far the enemy attacks from.")]
    public float range;

    [Tooltip("How far the enemy attacks from.")]
    public CombatEffectSO effect;

    [Tooltip("Time between enemy attacks (50 = 1 second).")]
    public int attackInterval;
}
