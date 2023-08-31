using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int GetItemIndex(ItemSO item)
    {
        int i = 0;
        foreach (InventoryItem inventoryItem in GetOrderedList())
        {
            if (inventoryItem.itemSO.itemCode == item.itemCode)
            {
                return i++;
            }
        }
        return -1;
    }

    public List<InventoryItem> GetOrderedList()
    {
        return Items.Values.OrderBy(i => i.itemSO.tier).ThenBy(i => i.itemSO.itemDisplayName).ToList();
    }
}
