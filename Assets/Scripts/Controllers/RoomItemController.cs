using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberTransform;
    [SerializeField] private Image iconTransform;
    [SerializeField] private TextMeshProUGUI nameTransform;

    public void SetItemUI(int number, Sprite image, string name)
    {
        SetNumber(number);
        SetImage(image);
        SetName(name);
    }
    public void Scale(float scale)
    {
        numberTransform.transform.localScale *= scale;
        iconTransform.transform.localScale *= scale;
        nameTransform.transform.localScale *= scale;
        numberTransform.transform.localPosition *= scale;
        iconTransform.transform.localPosition *= scale;
        nameTransform.transform.localPosition *= scale;
    }

    public void SetNumber(int number)
    {
        numberTransform.text = number.ToString();
    }

    public void SetImage(Sprite image)
    {
        iconTransform.sprite = image;
    }

    public void SetName(string name)
    {
        nameTransform.text = name;
    }
}
