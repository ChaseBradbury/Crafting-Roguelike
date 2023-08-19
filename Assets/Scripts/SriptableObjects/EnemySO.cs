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
}
