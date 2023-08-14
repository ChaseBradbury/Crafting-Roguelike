using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<string, InventoryItem> items;
    public Dictionary<string, InventoryItem> Items { get => items; set => items = value; }

    
    public Inventory()
    {
        items = new Dictionary<string, InventoryItem>();
    }

    public void AddItem(ItemSO item, int amount = 1)
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

    public void RemoveItem(ItemSO item, int amount = 1)
    {
        if (items.ContainsKey(item.itemCode))
        {
            items[item.itemCode].amount -= amount;
            if (items[item.itemCode].amount <= 0)
            {
                items.Remove(item.itemCode);
            }
        }
    }
}
