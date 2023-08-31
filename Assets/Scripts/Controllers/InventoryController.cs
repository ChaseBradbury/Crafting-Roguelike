using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] Vector2 slotSize;
    [SerializeField] int rowLength = 6;
    [SerializeField] Vector2 offset;
    private bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized && PlayerManager.Inventory != null)
        {
            RefreshUI();
            initialized = true;
        }
    }

    public void RefreshUI()
    {
        foreach (Transform child in inventoryContent) { Destroy(child.gameObject); }
        int x = 0;
        int y = 0;
        foreach (InventoryItem inventoryItem in PlayerManager.Inventory.GetOrderedList())
        {
            ItemSO item = inventoryItem.itemSO;
            int amount = inventoryItem.amount;
            RectTransform itemTransform = Instantiate(itemTemplate, inventoryContent).GetComponent<RectTransform>();
            itemTransform.transform.name = item.itemCode;
            itemTransform.gameObject.SetActive(true);
            itemTransform.anchoredPosition = new Vector2(x*slotSize.x + offset.x, -y*slotSize.y - offset.y);
            itemTransform.GetComponent<InventorySlotController>().AddSlotItem(item);
            itemTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemDisplayName;
            if (amount > 1)
            {
                Transform amountTransform = itemTransform.Find("Amount");
                amountTransform.gameObject.SetActive(true);
                amountTransform.Find("Number").GetComponent<TextMeshProUGUI>().text = amount.ToString();
            }
            ++x;
            if (x >= rowLength)
            {
                x = 0;
                ++y;
            }
        }
    }

    public Transform GetTransformOfInventoryItem(ItemSO item)
    {
        return inventoryContent.Find(item.itemCode);
    }

    public Vector2 GetPositionOfInventoryItem(ItemSO item)
    {
        Transform itemTransform = GetTransformOfInventoryItem(item);
        RectTransform rectTransform = itemTransform.GetComponent<RectTransform>();
        float x = itemTransform.position.x + rectTransform.rect.width/2;
        float y = itemTransform.position.y - rectTransform.rect.height/2;
        return new Vector2(x, y);
    }
}
