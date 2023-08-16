using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
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
    [SerializeField] private SlotController outputSlot;
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
            outputSlot.AddSlotItem(craftedItem);
        }
        else
        {
            outputSlot.RemoveSlotItem();
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
        ItemSO[] items = new ItemSO[]{north, east, south, west};

        if (!positionMatters)
        {
            items = items.OrderBy(item => item == null ? -1 : item.tier).ThenBy(item => item == null ? "" : item.itemDisplayName).ToArray();
        }

        string code = "";
        foreach (ItemSO item in items)
        {
            code += item == null ? " " : item.itemCode;
        }
        return code;
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
            case CraftingSlotDirection.Output:
                return outputSlot;
            default:
                return null;
                    
        }
    }

    public ItemSO Craft()
    {
        ItemSO outputItem = outputSlot.RemoveSlotItem();
        RemoveFromSlot(CraftingSlotDirection.North);
        RemoveFromSlot(CraftingSlotDirection.East);
        RemoveFromSlot(CraftingSlotDirection.South);
        RemoveFromSlot(CraftingSlotDirection.West);
        return outputItem;
    }
}
