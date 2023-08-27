using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotController : SlotController<ItemSO>
{
    public override void Select()
    {
        AudioManager.PlayLiftSound();
        craftingManager.OnMouseDownInventoryItem(itemSO);
    }
}
