using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlotController : SlotController<ItemSO>
{

    public override void Drag()
    {
        AudioManager.PlayLiftSound();
        craftingManager.OnMouseDownCrafting(direction);
    }
    
    public override void Drop()
    {
        // craftingManager.OnMouseUpCrafting(direction);
    }
}
