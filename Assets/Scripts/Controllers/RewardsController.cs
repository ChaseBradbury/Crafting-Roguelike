using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    [SerializeField] private Transform listItemTemplate;
    [SerializeField] private Transform rewardList;
    [SerializeField] private float listItemDistance;
    [SerializeField] private float listItemScale;
    private Transform hideableTransform;

    void Start()
    {
        hideableTransform = transform.Find("Hideable");
        int i = 0;
        foreach (RewardOption rewardOption in PlayerManager.CurrentRoom.rewards)
        {
            Transform itemTransform = Instantiate(listItemTemplate, rewardList);
            itemTransform.localPosition = new Vector3(0, -i * listItemDistance, 0);
            RoomItemController itemController = itemTransform.GetComponent<RoomItemController>();
            itemController.SetItemUI(rewardOption.GetNumber(), rewardOption.element.icon, rewardOption.element.name);
            itemController.Scale(listItemScale);
            itemTransform.gameObject.SetActive(true);
            ++i;
        }
    }

    public void OpenRewardsScreen()
    {
        hideableTransform.gameObject.SetActive(true);
    }

    public void Continue()
    {
        PlayerManager.BeatLevel();
    }
}
