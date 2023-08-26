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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SlotDirection craftingDirection = craftingController.GetClosestSlot(Input.mousePosition);
            SlotDirection weaponDirection = weaponController.GetClosestSlot(Input.mousePosition);
            if (craftingDirection != SlotDirection.Null)
            {
                OnMouseUpCrafting(craftingDirection);
            }
            else if (weaponDirection != SlotDirection.Null)
            {
                OnMouseUpWeapon(weaponDirection);
            }
            else
            {
                OnMouseUpInventory();
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

    public void HoverSlot(ItemSO item)
    {
        if (item != null)
        {
            dndCursor.HoverItem(item);
        }
    }

    public void LeaveSlot()
    {
        dndCursor.LeaveItem();
    }
}
