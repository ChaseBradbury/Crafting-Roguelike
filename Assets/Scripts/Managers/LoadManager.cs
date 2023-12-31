using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private ItemSO[] startingInventory;

    public void NewGame()
    {
        PlayerManager.CreateInventory(startingInventory);
        PlayerManager.Weapon = startingWeapon;
        PlayerManager.StartGame();
    }

    public void LoadNew()
    {
        PlayerManager.CreateInventory(startingInventory);
        PlayerManager.Weapon = startingWeapon;
    }
}
