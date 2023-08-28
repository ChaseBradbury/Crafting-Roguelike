using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    private Transform iconTransform;
    private Transform tooltipTransform;
    private Transform titleTransform;
    private Transform contentTransform;

    void Awake()
    {
        FollowMouse();
        iconTransform = transform.Find("Icon");
        tooltipTransform = transform.Find("Tooltip");
        titleTransform = tooltipTransform.Find("Title");
        contentTransform = tooltipTransform.Find("Content");
    }

    void Update()
    {
        FollowMouse();
    }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }

    public void SelectItem(ItemSO item)
    {
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<Image>().sprite = item.icon;
    }

    public void DropItem()
    {
        iconTransform.gameObject.SetActive(false);
    }

    public void HoverItem(ItemSO item)
    {
        tooltipTransform.gameObject.SetActive(true);
        string description = item.description;
        string name = item.itemDisplayName;
        ElementSO itemAsElement = item as ElementSO;
        if (itemAsElement != null)
        {
            description += "\nWhen imbued on a staff: " + itemAsElement.combatEffect.description;
        }
        FragmentSO itemAsFragment = item as FragmentSO;
        if (itemAsFragment != null)
        {
            name = itemAsFragment.imbuement.combatEffect.fragmentModularName + " " + itemAsFragment.fragmentDisplay.fragmentModularName;
            description = itemAsFragment.imbuement.combatEffect.description + "\n" + itemAsFragment.fragmentDisplay.description;
        }
        titleTransform.GetComponent<TextMeshProUGUI>().text = name;
        contentTransform.GetComponent<TextMeshProUGUI>().text = description;
    }

    public void LeaveItem()
    {
        tooltipTransform.gameObject.SetActive(false);
    }
}
