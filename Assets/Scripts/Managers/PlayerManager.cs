using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Inventory playerInventory;
    private ItemSO heldItem;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CraftingController craftingController;
    [SerializeField] private ItemSO[] testList;
    [SerializeField] private Image dndCursor;


    // Start is called before the first frame update
    void Start()
    {
        playerInventory = new Inventory();
        foreach (ItemSO item in testList)
        {
            playerInventory.AddItem(item, 1);
        }
        inventoryController.SetInventory(playerInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CraftingSlotDirection direction = craftingController.GetClosestSlot(Input.mousePosition);
            if (direction == CraftingSlotDirection.Null)
            {
                OnMouseUpInventory();
            }
            else
            {
                OnMouseUpCrafting(direction);
            }
        }
    }

    public void OnMouseDownInventoryItem(ItemSO item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            playerInventory.RemoveItem(item, 1);
            dndCursor.gameObject.SetActive(true);
            dndCursor.GetComponent<Image>().sprite = item.icon;
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpInventory()
    {
        if (heldItem != null)
        {
            playerInventory.AddItem(heldItem, 1);
            dndCursor.gameObject.SetActive(false);
            heldItem = null;
        }
        
        inventoryController.RefreshUI();
    }

    public void OnMouseUpCrafting(CraftingSlotDirection direction)
    {
        if (heldItem != null && craftingController.IsSlotEmpty(direction))
        {
            craftingController.AddToSlot(direction, heldItem);
            dndCursor.gameObject.SetActive(false);
            heldItem = null;
        }
    }

    public void OnMouseDownCrafting(CraftingSlotDirection direction)
    {
        if (heldItem == null && !craftingController.IsSlotEmpty(direction))
        {
            ItemSO item = craftingController.RemoveFromSlot(direction);
            heldItem = item;
            dndCursor.gameObject.SetActive(true);
            dndCursor.GetComponent<Image>().sprite = item.icon;
        }
    }

    public void OnMouseUpCraftingNorth() => OnMouseUpCrafting(CraftingSlotDirection.North);
    public void OnMouseUpCraftingEast() => OnMouseUpCrafting(CraftingSlotDirection.East);
    public void OnMouseUpCraftingSouth() => OnMouseUpCrafting(CraftingSlotDirection.South);
    public void OnMouseUpCraftingWest() => OnMouseUpCrafting(CraftingSlotDirection.West);
    public void OnMouseDownCraftingNorth() => OnMouseDownCrafting(CraftingSlotDirection.North);
    public void OnMouseDownCraftingEast() => OnMouseDownCrafting(CraftingSlotDirection.East);
    public void OnMouseDownCraftingSouth() => OnMouseDownCrafting(CraftingSlotDirection.South);
    public void OnMouseDownCraftingWest() => OnMouseDownCrafting(CraftingSlotDirection.West);
}
