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
    private static int currentLevel = 0;
    private static RoomSO currentRoom;
    public static Inventory Inventory { get => inventory; set => inventory = value; }
    public static Weapon Weapon { get => weapon; set => weapon = value; }
    public static RoomSO CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static bool tutorialComplete = false;
    public static bool levelOver = false;
    public static bool paused = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.GetInt("tutorialComplete", 0) == 1)
        {
            tutorialComplete = true;
        }
    }

    void Start()
    {
        
    }

    public static bool IsPaused()
    {
        return levelOver || !tutorialComplete || paused;
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
        AudioManager.PlayButtonSound();
        SceneManager.LoadScene("CombatScene");
    }

    public static void BeatLevel()
    {
        foreach (RewardOption reward in currentRoom.rewards)
        {
            inventory.AddItem(reward.element, reward.GetNumber());
        }
        ++currentLevel;
        levelOver = false;
        AudioManager.PlayButtonSound();
        SceneManager.LoadScene("CraftingScene");
    }

    public static void LoseLevel()
    {
        if (currentLevel > LoadHighscore())
        {
            SaveHighscore(currentLevel);
        }
        Reset();
        AudioManager.PlayButtonSound();
        SceneManager.LoadScene("MainMenuScene");
    }
    public static void Reset()
    {
        currentLevel = 0;
        currentRoom = null;
        levelOver = false;
        paused = false;
    }

    public static void StartGame()
    {
        AudioManager.PlayButtonSound();
        SceneManager.LoadScene("CraftingScene");
    }

    public static void SaveHighscore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
        PlayerPrefs.Save();
    }

    public static int LoadHighscore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public static void CompleteTutorial(bool isComplete)
    {
        if (isComplete)
        {
            PlayerPrefs.SetInt("tutorialComplete", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tutorialComplete", 0);
        }
        tutorialComplete = isComplete;
    }
}
