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

    [Tooltip("Where the item is relative to other items in the inventory (ascending).")]
    public int displayOrder;
}
