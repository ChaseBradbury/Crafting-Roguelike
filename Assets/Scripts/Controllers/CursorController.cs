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
        titleTransform.GetComponent<TextMeshProUGUI>().text = item.itemDisplayName;
        contentTransform.GetComponent<TextMeshProUGUI>().text = item.description;
    }

    public void LeaveItem()
    {
        tooltipTransform.gameObject.SetActive(false);
    }
}
