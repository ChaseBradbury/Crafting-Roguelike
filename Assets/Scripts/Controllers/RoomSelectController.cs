using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectController : MonoBehaviour
{
    [SerializeField] private Transform roomSelectTemplate;
    [SerializeField] private Transform[] roomOptionAreas = new Transform[3];
    [SerializeField] private RoomSO[] possibleRooms;
    private int[] roomIndices = new int[3];
    private Transform hideableTransform;

    public void Start()
    {
        for (int i = 0; i < roomIndices.Length; ++i)
        {
            roomIndices[i] = Random.Range(0, possibleRooms.Length);
            CreateRoomOption(i);
        }
        hideableTransform = transform.Find("Hideable");
    }

    public void OpenRoomSelect()
    {
        hideableTransform.gameObject.SetActive(true);
        
        
    }

    public void CreateRoomOption(int index)
    {
        RoomSelectOptionController selectOptionController = Instantiate(roomSelectTemplate, roomOptionAreas[index]).GetComponent<RoomSelectOptionController>();
        selectOptionController.SetRoom(possibleRooms[roomIndices[index]]);
        selectOptionController.gameObject.SetActive(true);
    }
}
