using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Room")]
public class RoomSO : ScriptableObject
{
    public string roomName;
    public RewardOption[] rewards;
    public EnemyOption[] enemies;
    public int minLevel;
    public int maxLevel;
    public Color tileColor;

    public bool InPoolForLevel(int level)
    {
        return level >= minLevel && level <= maxLevel;
    }
}
