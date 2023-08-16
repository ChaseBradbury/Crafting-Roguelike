using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    [Tooltip("Unique code for the item.")]
    public string itemCode;

    [Tooltip("Name displayed in the inventory.")]
    public string itemDisplayName;

    [Tooltip("Image displayed in the inventory.")]
    public Sprite icon;

    [Tooltip("Longest crafting chain to make the item. Determines display order (ascending, followed by display name).")]
    public int tier;
}
