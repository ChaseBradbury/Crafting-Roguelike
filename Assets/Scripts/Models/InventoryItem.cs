using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public ItemSO itemSO;
    // Currently not stacking, but "amount" will be useful if I decide to
    public int amount;

    public InventoryItem(ItemSO itemSO, int amount)
    {
        this.itemSO = itemSO;
        this.amount = amount;
    }
}
