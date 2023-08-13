using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] float slotSize = 100f;
    [SerializeField] int rowLength = 6;
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
        int x = 0;
        int y = 0;
        foreach (KeyValuePair<string, InventoryItem> inventoryItem in inventory.Items.OrderBy(i => i.Value.itemSO.displayOrder))
        {
            RectTransform itemTransform = Instantiate(itemTemplate, inventoryContent).GetComponent<RectTransform>();
            itemTransform.gameObject.SetActive(true);
            itemTransform.anchoredPosition = new Vector2(x*slotSize, y*slotSize);
            ++x;
            if (x > rowLength)
            {
                x = 0;
                ++y;
            }
        }
    }
}
