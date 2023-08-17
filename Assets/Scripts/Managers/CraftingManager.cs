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
    [SerializeField] private ItemSO[] testList;
    [SerializeField] private CursorController dndCursor;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.CreateInventory(testList);
        inventoryController.SetInventory(PlayerManager.Inventory);
    }

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
            dndCursor.gameObject.SetActive(true);
            dndCursor.GetComponent<Image>().sprite = item.icon;
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpInventory()
    {
        if (heldItem != null)
        {
            PlayerManager.Inventory.AddItem(heldItem, 1);
            dndCursor.gameObject.SetActive(false);
            heldItem = null;
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpCrafting(SlotDirection direction)
    {
        if (heldItem != null && craftingController.IsSlotEmpty(direction))
        {
            craftingController.AddToSlot(direction, heldItem);
            dndCursor.gameObject.SetActive(false);
            heldItem = null;
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
            dndCursor.FollowMouse();
            dndCursor.gameObject.SetActive(true);
            dndCursor.GetComponent<Image>().sprite = item.icon;
        }
    }

    public void OnMouseDownOutput(ItemSO outputItem)
    {
        if (heldItem == null && !craftingController.IsSlotEmpty(SlotDirection.Output))
        {
            ItemSO item = craftingController.Craft();
            heldItem = item;
            dndCursor.FollowMouse();
            dndCursor.gameObject.SetActive(true);
            dndCursor.GetComponent<Image>().sprite = item.icon;
        }
    }

    public void OnMouseUpWeapon(SlotDirection direction)
    {
        if (heldItem != null && weaponController.WillAcceptItem(direction, heldItem))
        {
            weaponController.AddToSlot(direction, heldItem as FragmentSO);
            dndCursor.gameObject.SetActive(false);
            heldItem = null;
        }
        else
        {
            OnMouseUpInventory();
        }
    }
}
