using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private Transform craftingArea;
    [SerializeField] private float craftingAreaSize = 200f;
    [SerializeField] private CraftingSlotController northCraftingSlot;
    [SerializeField] private CraftingSlotController eastCraftingSlot;
    [SerializeField] private CraftingSlotController southCraftingSlot;
    [SerializeField] private CraftingSlotController westCraftingSlot;
    [SerializeField] private OutputSlotController outputSlot;
    [SerializeField] private RecipeSO[] recipes;

    public bool IsSlotEmpty(SlotDirection direction)
    {
        SlotController<ItemSO> slotController = GetSlotController(direction);
        if (slotController != null)
        {
            return slotController.IsSlotEmpty();
        }
        else
        {
            return false;
        }
    }

    public SlotDirection GetClosestSlot(Vector3 mousePosition)
    {
        float distance = Vector2.Distance(mousePosition, craftingArea.position);
        if (distance < craftingAreaSize)
        {
            return Utils.FindDirectionQuadrant(mousePosition, craftingArea.position);
        }
        else
        {
            return SlotDirection.Null;
        }
    }

    public void AddToSlot(SlotDirection direction, ItemSO item)
    {
        SlotController<ItemSO> slotController = GetSlotController(direction);
        if (slotController != null)
        {
            slotController.AddSlotItem(item);
        }
        
        UpdateCrafting();

    }

    public ItemSO RemoveFromSlot(SlotDirection direction)
    {
        SlotController<ItemSO> slotController = GetSlotController(direction);
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

    public SlotController<ItemSO> GetSlotController(SlotDirection direction)
    {
        switch (direction)
        {
            case SlotDirection.CraftingNorth:
                return northCraftingSlot;
            case SlotDirection.CraftingEast:
                return eastCraftingSlot;
            case SlotDirection.CraftingSouth:
                return southCraftingSlot;
            case SlotDirection.CraftingWest:
                return westCraftingSlot;
            case SlotDirection.Output:
                return outputSlot;
            default:
                return null;
                    
        }
    }

    public ItemSO Craft()
    {
        ItemSO outputItem = outputSlot.RemoveSlotItem();
        RemoveFromSlot(SlotDirection.CraftingNorth);
        RemoveFromSlot(SlotDirection.CraftingEast);
        RemoveFromSlot(SlotDirection.CraftingSouth);
        RemoveFromSlot(SlotDirection.CraftingWest);
        return outputItem;
    }
}
