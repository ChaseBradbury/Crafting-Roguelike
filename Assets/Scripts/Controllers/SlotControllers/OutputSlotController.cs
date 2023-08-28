using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputSlotController : SlotController<ItemSO>
{
    public override void Drag()
    {
        AudioManager.PlayCraftSound();
        craftingManager.OnMouseDownOutput(itemSO);
    }

    public override void Drop()
    {
        // Does nothing
    }
}
