using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotController : SlotController<ItemSO>
{
    public override void Drag()
    {
        AudioManager.PlayLiftSound();
        craftingManager.OnMouseDownInventoryItem(itemSO);
    }

    public override void Drop()
    {
        // Does nothing
    }
}
