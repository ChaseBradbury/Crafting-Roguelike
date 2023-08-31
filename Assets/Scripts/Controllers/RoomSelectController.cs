using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectController : MonoBehaviour
{
    [SerializeField] private Transform roomSelectTemplate;
    [SerializeField] private Transform[] roomOptionAreas = new Transform[3];
    [SerializeField] private RoomSO[] totalRooms;
    private List<RoomSO> possibleRooms;
    private int[] roomIndices = new int[3];
    private Transform hideableTransform;

    public void Start()
    {
        possibleRooms = new List<RoomSO>();
        foreach (RoomSO room in totalRooms)
        {
            if (room.InPoolForLevel(PlayerManager.CurrentLevel))
            {
                possibleRooms.Add(room);
            }
        }
        Utils.Shuffle(possibleRooms);
        for (int i = 0; i < roomIndices.Length; ++i)
        {
            roomIndices[i] = Random.Range(0, possibleRooms.Count);
            CreateRoomOption(i);
        }
        hideableTransform = transform.Find("Hideable");
    }

    public void OpenRoomSelect()
    {
        CraftingManager.Instance.EmptyCraftingTable();
        hideableTransform.gameObject.SetActive(true);
    }

    public void CreateRoomOption(int index)
    {
        RoomSelectOptionController selectOptionController = Instantiate(roomSelectTemplate, roomOptionAreas[index]).GetComponent<RoomSelectOptionController>();
        if (possibleRooms.Count > index)
        {
            selectOptionController.SetRoom(possibleRooms[index]);
        }
        else
        {
            selectOptionController.SetRoom(possibleRooms[possibleRooms.Count - 1]);
        }
        selectOptionController.gameObject.SetActive(true);
    }
}
