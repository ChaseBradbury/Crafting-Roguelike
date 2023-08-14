using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private Transform CraftingArea;
    [SerializeField] private float craftingAreaSize = 200f;
    [SerializeField] private SlotController northCraftingSlot;
    [SerializeField] private SlotController eastCraftingSlot;
    [SerializeField] private SlotController southCraftingSlot;
    [SerializeField] private SlotController westCraftingSlot;
    [SerializeField] private SlotController OutputSlot;
    [SerializeField] private RecipeSO[] recipes;

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

    public CraftingSlotDirection GetClosestSlot(Vector3 mousePosition)
    {
        float distance = Vector2.Distance(mousePosition, CraftingArea.position);
        if (distance < craftingAreaSize)
        {
            float xDist = mousePosition.x - CraftingArea.position.x;
            float yDist = mousePosition.y - CraftingArea.position.y;
            if (xDist > yDist)
            {
                // In SE
                if (xDist + yDist > 0)
                {
                    return CraftingSlotDirection.East;
                }
                else
                {
                    return CraftingSlotDirection.South;
                }
            }
            else
            {
                // In NW
                if (xDist + yDist > 0)
                {
                    return CraftingSlotDirection.North;
                }
                else
                {
                    return CraftingSlotDirection.West;
                }
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
        
        UpdateCrafting();

    }

    public ItemSO RemoveFromSlot(CraftingSlotDirection direction)
    {
        SlotController slotController = GetSlotController(direction);
        ItemSO item = null;
        if (slotController != null)
        {
            item = slotController.RemoveSlotItem();
            UpdateCrafting();
        }
        return item;
    }

    public void UpdateCrafting()
    {
        ItemSO craftedItem = GetCraftedItem();
        if(craftedItem != null)
        {
            OutputSlot.AddSlotItem(craftedItem);
        }
        else
        {
            OutputSlot.RemoveSlotItem();
        }
    }

    public ItemSO GetCraftedItem()
    {
        string craftingCode = GetCraftingCode(northCraftingSlot.ItemSO, eastCraftingSlot.ItemSO, southCraftingSlot.ItemSO, westCraftingSlot.ItemSO, false);
        string craftingCodePositional = GetCraftingCode(northCraftingSlot.ItemSO, eastCraftingSlot.ItemSO, southCraftingSlot.ItemSO, westCraftingSlot.ItemSO, true);
        ItemSO outputItem = null;
        foreach (RecipeSO recipe in recipes)
        {
            string recipeCode = GetCraftingCodeFromRecipe(recipe);
            if (recipe.positionMatters)
            {
                if (recipeCode == craftingCodePositional)
                {
                    outputItem = recipe.outputItem;
                }
            }
            else
            {
                if (recipeCode == craftingCode)
                {
                    outputItem = recipe.outputItem;
                }
            }
        }
        return outputItem;
    }

    public string GetCraftingCodeFromRecipe(RecipeSO recipe)
    {
        return GetCraftingCode(recipe.northSlot, recipe.eastSlot, recipe.southSlot, recipe.westSlot, recipe.positionMatters);
    }

    public string GetCraftingCode(ItemSO north, ItemSO east, ItemSO south, ItemSO west, bool positionMatters)
    {
        string northCode = north == null ? " " : north.itemCode;
        string eastCode = east == null ? " " : east.itemCode;
        string southCode = south == null ? " " : south.itemCode;
        string westCode = west == null ? " " : west.itemCode;
        // if (positionMatters)
        {
            return northCode + eastCode + southCode + westCode;
        }
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
