using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlotController : SlotController<ItemSO>
{
    [SerializeField] private SlotDirection direction;

    public override void Select()
    {
        AudioManager.PlayLiftSound();
        craftingManager.OnMouseDownCrafting(direction);
    }
}
