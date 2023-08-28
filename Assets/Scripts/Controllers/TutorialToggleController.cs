using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggleController : MonoBehaviour
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    [SerializeField] private Image checkBackground;
    [SerializeField] private GameObject checkmarkObject;

    public void Start()
    {
        UpdateUI(PlayerManager.tutorialComplete);
    }

    public void Toggle()
    {
        PlayerManager.tutorialComplete = !PlayerManager.tutorialComplete;
        UpdateUI(PlayerManager.tutorialComplete);
    }

    public void UpdateUI(bool hideTutorial)
    {
        if (hideTutorial)
        {
            checkBackground.color = offColor;
            checkmarkObject.SetActive(false);
        }
        else
        {
            checkBackground.color = onColor;
            checkmarkObject.SetActive(true);
        }
    }
}
