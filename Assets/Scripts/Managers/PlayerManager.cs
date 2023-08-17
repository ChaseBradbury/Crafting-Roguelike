using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private static Inventory inventory;
    public static Inventory Inventory { get => inventory; set => inventory = value; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void CreateInventory(ItemSO[] items)
    {
        inventory = new Inventory();
        foreach (ItemSO item in items)
        {
            inventory.AddItem(item, 1);
        }
    }

    public static void MoveToCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public static void MoveToCraftingScene()
    {
        SceneManager.LoadScene("CraftingScene");
    }
}
