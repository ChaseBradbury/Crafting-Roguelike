using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CursorFlyController : MonoBehaviour
{
    [SerializeField] private int flyFrames;
    private Transform iconTransform;
    private Vector2 targetPosition;
    private int currentFrame = 0;

    void Awake()
    {
        iconTransform = transform.Find("Icon");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentFrame < flyFrames)
        {
            float ratio = currentFrame / (float)flyFrames;
            transform.position = Vector2.Lerp(transform.position, targetPosition, ratio);
            ++currentFrame;
        }
        else
        {
            iconTransform.gameObject.SetActive(false);
        }
    }

    public void Fly(Vector2 startPosition, Vector2 endPosition, ItemSO item)
    {
        transform.position = startPosition;
        targetPosition = endPosition;
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<Image>().sprite = item.icon;
        currentFrame = 0;
    }
}
