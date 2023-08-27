using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotController<T> : MonoBehaviour where T : ItemSO
{
    protected T itemSO;
    [SerializeField] protected Sprite emptySprite;
    [SerializeField] protected CraftingManager craftingManager;
    private List<TutorialStep> tutorialSteps;


    public T ItemSO { get => itemSO; set => itemSO = value; }

    public void Awake()
    {
        tutorialSteps = new List<TutorialStep>();
    }

    public bool IsSlotEmpty()
    {
        return itemSO == null;
    }

    public void AddSlotItem(T item)
    {
        CheckTutorialSteps(true);
        itemSO = item;
        transform.Find("Icon").GetComponent<Image>().sprite = itemSO.icon;
        
    }

    public T RemoveSlotItem()
    {
        T tmpItem = itemSO;
        itemSO = null;
        transform.Find("Icon").GetComponent<Image>().sprite = emptySprite;
        return tmpItem;
    }

    public void Hover()
    {
        craftingManager.HoverSlot(itemSO);
    }

    public void Leave()
    {
        craftingManager.LeaveSlot();
    }

    public void OnMouseDown()
    {
        CheckTutorialSteps(false);
        Select();
    }

    public abstract void Select();

    public void AddTutorialStep(TutorialStep step)
    {
        tutorialSteps.Add(step);
    }

    public void CheckTutorialSteps(bool onPlace)
    {
        foreach (TutorialStep step in tutorialSteps)
        {
            if (step.IsCurrent && (step.completeOnPlace == onPlace))
            {
                step.Completed = true;
            }
        }
    }
}
