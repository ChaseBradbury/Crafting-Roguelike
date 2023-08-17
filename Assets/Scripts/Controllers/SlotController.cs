using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotController<T> : MonoBehaviour where T : ItemSO
{
    protected T itemSO;
    [SerializeField] protected Sprite emptySprite;
    [SerializeField] protected CraftingManager craftingManager;


    public T ItemSO { get => itemSO; set => itemSO = value; }

    public bool IsSlotEmpty()
    {
        return itemSO == null;
    }

    public void AddSlotItem(T item)
    {
        itemSO = item;
        transform.Find("Icon").GetComponent<Image>().sprite = itemSO.icon;
    }

    public T RemoveSlotItem()
    {
        T tmpItem = itemSO;
        itemSO = null;
        transform.Find("Icon").GetComponent<Image>().sprite = emptySprite;
        return tmpItem;
    }

    public abstract void OnMouseDown();

}
