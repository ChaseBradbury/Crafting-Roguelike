using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, InventoryItem> items;
    public Dictionary<string, InventoryItem> Items { get => items; set => items = value; }

    public void AddItem(ItemSO item, int amount)
    {
        if (items.ContainsKey(item.itemCode))
        {
            items[item.itemCode].amount += amount;
        }
        else
        {
            Items.Add(item.itemCode, new InventoryItem(item, amount));
        }
    }
}
