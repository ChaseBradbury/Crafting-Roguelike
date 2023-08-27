using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputSlotController : SlotController<ItemSO>
{
    public override void Select()
    {
        AudioManager.PlayCraftSound();
        craftingManager.OnMouseDownOutput(itemSO);
    }
}
