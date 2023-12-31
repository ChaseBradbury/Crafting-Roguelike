using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectOptionController : MonoBehaviour
{
    [SerializeField] private Transform roomListItemTemplate;
    [SerializeField] private Transform enemyList;
    [SerializeField] private Transform rewardList;
    [SerializeField] private float listItemDistance;
    private RoomSO room;

    public void SetRoom(RoomSO room)
    {
        this.room = room;
        transform.Find("Title").GetComponent<TextMeshProUGUI>().text = room.roomName;
        transform.Find("Image").GetComponent<Image>().color = room.tileColor;
        int i = 0;
        foreach (EnemyOption enemyOption in room.enemies)
        {
            Transform itemTransform = Instantiate(roomListItemTemplate, enemyList);
            itemTransform.localPosition = new Vector3(0, -i * listItemDistance, 0);
            itemTransform.GetComponent<RoomItemController>().SetItemUI(enemyOption.GetNumber(room), enemyOption.enemy.sprite, enemyOption.enemy.enemyName);
            itemTransform.gameObject.SetActive(true);
            ++i;
        }
        i = 0;
        foreach (RewardOption rewardOption in room.rewards)
        {
            Transform itemTransform = Instantiate(roomListItemTemplate, rewardList);
            itemTransform.localPosition = new Vector3(0, -i * listItemDistance, 0);
            itemTransform.GetComponent<RoomItemController>().SetItemUI(rewardOption.GetNumber(), rewardOption.element.icon, rewardOption.element.name);
            itemTransform.gameObject.SetActive(true);
            ++i;
        }
    }

    public void ChooseRoom()
    {
        PlayerManager.MoveToCombatScene(room);
    }
}
