using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private ItemSO[] startingInventory;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.CreateInventory(startingInventory);
        PlayerManager.Weapon = startingWeapon;
    }
}
