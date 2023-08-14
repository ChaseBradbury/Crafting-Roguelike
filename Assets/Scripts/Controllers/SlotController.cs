using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private ItemSO itemSO;

    public ItemSO ItemSO { get => itemSO; set => itemSO = value; }

    public bool IsSlotEmpty()
    {
        return itemSO == null;
    }

    public void AddSlotItem(ItemSO item)
    {
        itemSO = item;
        gameObject.GetComponent<Image>().sprite = itemSO.icon;
    }

    public ItemSO RemoveSlotItem()
    {
        ItemSO tmpItem = itemSO;
        itemSO = null;
        gameObject.GetComponent<Image>().sprite = null;
        return tmpItem;
    }
}
