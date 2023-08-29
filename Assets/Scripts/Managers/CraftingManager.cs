using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    // Rename to CraftingInputManager and make the fields static? Possibly change the controllers to static as well since there will only be one
    public static CraftingManager Instance;
    private ItemSO heldItem;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CraftingController craftingController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private CursorController dndCursor;

    private SlotDirection hoveredSlot;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            switch (hoveredSlot)
            {
                case SlotDirection.CraftingNorth:
                case SlotDirection.CraftingSouth:
                case SlotDirection.CraftingWest:
                case SlotDirection.CraftingEast:
                case SlotDirection.Output:
                    OnMouseUpCrafting(hoveredSlot);
                    break;
                case SlotDirection.North:
                case SlotDirection.South:
                case SlotDirection.West:
                case SlotDirection.East:
                case SlotDirection.Base:
                    OnMouseUpWeapon(hoveredSlot);
                    break;
                default:
                    OnMouseUpInventory();
                    break;

            }
        }
    }

    public void OnMouseDownInventoryItem(ItemSO item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            PlayerManager.Inventory.RemoveItem(item, 1);
            dndCursor.SelectItem(item);
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpInventory()
    {
        if (heldItem != null)
        {
            PlayerManager.Inventory.AddItem(heldItem, 1);
            dndCursor.DropItem();
            heldItem = null;
            AudioManager.PlayPlaceSound();
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpCrafting(SlotDirection direction)
    {
        if (heldItem != null && craftingController.IsSlotEmpty(direction))
        {
            craftingController.AddToSlot(direction, heldItem);
            dndCursor.DropItem();
            heldItem = null;
            AudioManager.PlayPlaceSound();
        }
        else
        {
            OnMouseUpInventory();
        }
    }

    public void OnMouseDownCrafting(SlotDirection direction)
    {
        if (heldItem == null && !craftingController.IsSlotEmpty(direction))
        {
            ItemSO item = craftingController.RemoveFromSlot(direction);
            heldItem = item;
            dndCursor.SelectItem(item);
        }
    }

    public void OnMouseDownOutput(ItemSO outputItem)
    {
        if (heldItem == null && !craftingController.IsSlotEmpty(SlotDirection.Output))
        {
            ItemSO item = craftingController.Craft();
            heldItem = item;
            dndCursor.SelectItem(item);
        }
    }

    public void OnMouseUpWeapon(SlotDirection direction)
    {
        if (heldItem != null && weaponController.WillAcceptItem(direction, heldItem))
        {
            weaponController.AddToSlot(direction, heldItem as FragmentSO);
            dndCursor.DropItem();
            heldItem = null;
        }
        else
        {
            OnMouseUpInventory();
        }
    }

    public void HoverSlot(SlotDirection direction, ItemSO item)
    {
        hoveredSlot = direction;
        if (item != null)
        {
            dndCursor.HoverItem(item);
        }
    }

    public void LeaveSlot(SlotDirection direction)
    {
        if (hoveredSlot == direction)
        {
            hoveredSlot = SlotDirection.Null;
        }
        dndCursor.LeaveItem();
    }

    public void EmptyCraftingTable()
    {
        if (!craftingController.IsSlotEmpty(SlotDirection.CraftingNorth))
        {
            PlayerManager.Inventory.AddItem(craftingController.RemoveFromSlot(SlotDirection.CraftingNorth), 1);
        }
        if (!craftingController.IsSlotEmpty(SlotDirection.CraftingSouth))
        {
            PlayerManager.Inventory.AddItem(craftingController.RemoveFromSlot(SlotDirection.CraftingSouth), 1);
        }
        if (!craftingController.IsSlotEmpty(SlotDirection.CraftingEast))
        {
            PlayerManager.Inventory.AddItem(craftingController.RemoveFromSlot(SlotDirection.CraftingEast), 1);
        }
        if (!craftingController.IsSlotEmpty(SlotDirection.CraftingWest))
        {
            PlayerManager.Inventory.AddItem(craftingController.RemoveFromSlot(SlotDirection.CraftingWest), 1);
        }
    }
}
