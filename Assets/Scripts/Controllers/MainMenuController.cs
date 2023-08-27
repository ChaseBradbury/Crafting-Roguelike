using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    void Start()
    {
        score.text = PlayerManager.LoadHighscore().ToString();
    }

    public void ResetHighscore()
    {
        int resetScore = 0;
        PlayerManager.SaveHighscore(resetScore);
        score.text = resetScore.ToString();
    }
}
