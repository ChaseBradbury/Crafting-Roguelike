using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Transform highscoreTransform;
    [SerializeField] private TextMeshProUGUI score;
    private Transform hideableTransform;
    void Start()
    {
        hideableTransform = transform.Find("Hideable");
    }

    public void OpenGameOverScreen()
    {
        hideableTransform.gameObject.SetActive(true);
        
        if (PlayerManager.CurrentLevel > PlayerManager.LoadHighscore())
        {
            highscoreTransform.gameObject.SetActive(true);
        }
        score.text = PlayerManager.CurrentLevel.ToString();
    }

    public void MainMenu()
    {
        PlayerManager.LoseLevel();
    }
}
