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
    [SerializeField] float slotSize = 110f;
    [SerializeField] int rowLength = 6;
    [SerializeField] float offset = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in inventoryContent) { Destroy(child.gameObject); }
        int x = 0;
        int y = 0;
        foreach (KeyValuePair<string, InventoryItem> inventoryItem in inventory.Items.OrderBy(i => i.Value.itemSO.tier).ThenBy(i => i.Value.itemSO.itemDisplayName))
        {
            ItemSO item = inventoryItem.Value.itemSO;
            int amount = inventoryItem.Value.amount;
            RectTransform itemTransform = Instantiate(itemTemplate, inventoryContent).GetComponent<RectTransform>();
            itemTransform.gameObject.SetActive(true);
            itemTransform.anchoredPosition = new Vector2(x*slotSize + offset, -y*slotSize - offset);
            itemTransform.GetComponent<SlotController>().AddSlotItem(item);
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
}
