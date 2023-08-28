using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialStep[] steps;
    [SerializeField] private RectTransform tutorialContainer;
    private int currentStep = 0;
    private RectTransform tooltipTransform;
    private TextMeshProUGUI titleObject;
    private TextMeshProUGUI contentObject;
    private RectTransform arrowTransform;

    public void Start()
    {
        tooltipTransform = tutorialContainer.Find("Tooltip").GetComponent<RectTransform>();
        arrowTransform = tutorialContainer.Find("Arrow").GetComponent<RectTransform>();
        titleObject = tooltipTransform.Find("Title").GetComponent<TextMeshProUGUI>();
        contentObject = tooltipTransform.Find("Content").GetComponent<TextMeshProUGUI>();
        foreach (TutorialStep step in steps)
        {
            //step.controller.AddTutorialStep(step);
        }
        if (steps.Length > currentStep)
        {
            steps[currentStep].IsCurrent = true;
        }
        tooltipTransform.gameObject.SetActive(!PlayerManager.tutorialComplete);
        arrowTransform.gameObject.SetActive(!PlayerManager.tutorialComplete);
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
                    arrowTransform.gameObject.SetActive(!PlayerManager.tutorialComplete);
                }
            }
        }
    }

    public void UpdateUI()
    {
        tutorialContainer.position = steps[currentStep].transform.position;
        switch (steps[currentStep].placement)
        {
            case MoveDirection.Left:
                tooltipTransform.pivot = new Vector2(1, .5f);
                Vector2 anchor = new Vector2(0, .5f);
                tooltipTransform.anchorMax = anchor;
                tooltipTransform.anchorMin = anchor;
                arrowTransform.anchorMax = anchor;
                arrowTransform.anchorMin = anchor;
                break;
            case MoveDirection.Right:
                tooltipTransform.pivot = new Vector2(0, .5f);
                anchor = new Vector2(1, .5f);
                tooltipTransform.anchorMax = anchor;
                tooltipTransform.anchorMin = anchor;
                arrowTransform.anchorMax = anchor;
                arrowTransform.anchorMin = anchor;
                break;
            case MoveDirection.Up:
                tooltipTransform.pivot = new Vector2(.5f, 0);
                anchor = new Vector2(.5f, 1);
                tooltipTransform.anchorMax = anchor;
                tooltipTransform.anchorMin = anchor;
                arrowTransform.anchorMax = anchor;
                arrowTransform.anchorMin = anchor;
                break;
            case MoveDirection.Down:
                tooltipTransform.pivot = new Vector2(.5f, 1);
                anchor = new Vector2(.5f, 0);
                tooltipTransform.anchorMax = anchor;
                tooltipTransform.anchorMin = anchor;
                arrowTransform.anchorMax = anchor;
                arrowTransform.anchorMin = anchor;
                break;
        }
        tooltipTransform.localPosition = new Vector2(0, 0);
        arrowTransform.localPosition = new Vector2(0, 0);
        titleObject.text = steps[currentStep].title;
        contentObject.text = steps[currentStep].content;
    }

    public void CompleteCurrentStep()
    {
        steps[currentStep].Completed = true;
    }
}
