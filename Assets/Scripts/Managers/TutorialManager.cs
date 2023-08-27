using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialStep[] steps;
    [SerializeField] private Transform tutorialContainer;
    private int currentStep = 0;
    private Transform tooltipTransform;
    private TextMeshProUGUI titleObject;
    private TextMeshProUGUI contentObject;

    public void Start()
    {
        tooltipTransform = tutorialContainer.Find("Tooltip");
        titleObject = tooltipTransform.Find("Title").GetComponent<TextMeshProUGUI>();
        contentObject = tooltipTransform.Find("Content").GetComponent<TextMeshProUGUI>();
        foreach (TutorialStep step in steps)
        {
            step.controller.AddTutorialStep(step);
        }
        if (steps.Length > currentStep)
        {
            steps[currentStep].IsCurrent = true;
        }
        tooltipTransform.gameObject.SetActive(!PlayerManager.tutorialComplete);
        UpdateUI();
    }

    public void Update()
    {
        if (!PlayerManager.tutorialComplete)
        {
            if (steps[currentStep].Completed)
            {
                steps[currentStep++].IsCurrent = false;
                if (steps.Length > currentStep)
                {
                    steps[currentStep].IsCurrent = true;
                    UpdateUI();
                }
                else
                {
                    PlayerManager.CompleteTutorial();
                    tooltipTransform.gameObject.SetActive(!PlayerManager.tutorialComplete);
                }
            }
        }
    }

    public void UpdateUI()
    {
        tutorialContainer.position = steps[currentStep].controller.transform.position;
        titleObject.text = steps[currentStep].title;
        contentObject.text = steps[currentStep].content;
    }
}
