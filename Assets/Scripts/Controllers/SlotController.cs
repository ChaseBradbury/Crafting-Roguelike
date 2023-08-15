using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private ItemSO itemSO;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private PlayerManager playerManager;


    public ItemSO ItemSO { get => itemSO; set => itemSO = value; }

    public bool IsSlotEmpty()
    {
        return itemSO == null;
    }

    public void AddSlotItem(ItemSO item)
    {
        itemSO = item;
        transform.Find("Icon").GetComponent<Image>().sprite = itemSO.icon;
    }

    public ItemSO RemoveSlotItem()
    {
        ItemSO tmpItem = itemSO;
        itemSO = null;
        transform.Find("Icon").GetComponent<Image>().sprite = emptySprite;
        return tmpItem;
    }

    public void OnMouseDownInventory()
    {
        playerManager.OnMouseDownInventoryItem(itemSO);
    }

    public void OnMouseDownOutput()
    {
        playerManager.OnMouseDownOutput(itemSO);
    }

}
