using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotController : SlotController<ItemSO>
{
    public override void OnMouseDown()
    {
        playerManager.OnMouseDownInventoryItem(itemSO);
    }
}
