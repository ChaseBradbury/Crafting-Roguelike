using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Room")]
public class RoomSO : ScriptableObject
{
    public string roomName;
    public int numberOfRewards;
    public RewardOption[] rewards;
    public int numberOfEnemies;
    public EnemyOption[] enemies;
}
