using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private static Inventory inventory;
    private static Weapon weapon;
    private static int currentLevel = 1;
    private static RoomSO currentRoom;
    public static Inventory Inventory { get => inventory; set => inventory = value; }
    public static Weapon Weapon { get => weapon; set => weapon = value; }
    public static RoomSO CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    public static void CreateInventory(ItemSO[] items)
    {
        inventory = new Inventory();
        foreach (ItemSO item in items)
        {
            inventory.AddItem(item, 1);
        }
    }

    public static void MoveToCombatScene(RoomSO room)
    {
        currentRoom = room;
        SceneManager.LoadScene("CombatScene");
    }

    public static void BeatLevel()
    {
        foreach (RewardOption reward in currentRoom.rewards)
        {
            inventory.AddItem(reward.element, reward.GetNumber());
        }
        ++currentLevel;
        SceneManager.LoadScene("CraftingScene");
    }

    public static void LoseLevel()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("CraftingScene");
    }
    
}
