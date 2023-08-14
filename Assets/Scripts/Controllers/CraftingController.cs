using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private SlotController northCraftingSlot;
    [SerializeField] private SlotController eastCraftingSlot;
    [SerializeField] private SlotController southCraftingSlot;
    [SerializeField] private SlotController westCraftingSlot;

    public bool IsSlotEmpty(CraftingSlotDirection direction)
    {
        SlotController slotController = GetSlotController(direction);
        if (slotController != null)
        {
            return slotController.IsSlotEmpty();
        }
        else
        {
            return false;
        }
    }

    public CraftingSlotDirection GetClosestSlot(Vector3 position)
    {
        float distance = Vector2.Distance(position, transform.position);
        if (distance < 200f)
        {
            if (position.y - transform.position.y > 0)
            {
                return CraftingSlotDirection.North;
            }
            else
            {
                return CraftingSlotDirection.South;
            }
        }
        else
        {
            return CraftingSlotDirection.Null;
        }
    }

    public void AddToSlot(CraftingSlotDirection direction, ItemSO item)
    {
        SlotController slotController = GetSlotController(direction);
        if (slotController != null)
        {
            slotController.AddSlotItem(item);
        }
    }

    public ItemSO RemoveFromSlot(CraftingSlotDirection direction)
    {
        SlotController slotController = GetSlotController(direction);
        if (slotController != null)
        {
            return slotController.RemoveSlotItem();
        }
        return null;
    }

    public void UpdateCrafting()
    {
        
    }

    public SlotController GetSlotController(CraftingSlotDirection direction)
    {
        switch (direction)
        {
            case CraftingSlotDirection.North:
                return northCraftingSlot;
            case CraftingSlotDirection.East:
                return eastCraftingSlot;
            case CraftingSlotDirection.South:
                return southCraftingSlot;
            case CraftingSlotDirection.West:
                return westCraftingSlot;
            default:
                return null;
                    
        }
    }
}
