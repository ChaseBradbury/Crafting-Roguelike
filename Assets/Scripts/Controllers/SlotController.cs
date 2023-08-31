using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotController<T> : MonoBehaviour  where T : ItemSO
{
    protected T itemSO;
    [SerializeField] protected Sprite emptySprite;
    [SerializeField] protected CraftingManager craftingManager;
    private List<TutorialStep> tutorialSteps;
    [SerializeField] protected SlotDirection direction;
    [SerializeField] protected Sprite backgroundSprite;
    private Image icon;
    private Image background;


    public T ItemSO { get => itemSO; set => itemSO = value; }

    public void Awake()
    {
        tutorialSteps = new List<TutorialStep>();
        icon = transform.Find("Icon").GetComponent<Image>();
        Transform bgTransform = transform.Find("Background");
        if (bgTransform != null)
        {
            background = transform.Find("Background").GetComponent<Image>();
        }
    }

    public bool IsSlotEmpty()
    {
        return itemSO == null;
    }

    public void AddSlotItem(T item)
    {
        CheckTutorialSteps(true);
        itemSO = item;
        icon.sprite = itemSO.icon;
        if (background != null)
        {
            FragmentSO fragment = item as FragmentSO;
            if (fragment != null)
            {
                background.sprite = fragment.fragmentDisplay.background;
            }
            else
            {
                background.sprite = backgroundSprite;
            }
        }
    }

    public T RemoveSlotItem()
    {
        T tmpItem = itemSO;
        itemSO = null;
        icon.sprite = emptySprite;
        if (background != null)
        {
            background.sprite = backgroundSprite;
        }
        return tmpItem;
    }

    public void Hover()
    {
        craftingManager.HoverSlot(direction, itemSO);
    }

    public void Leave()
    {
        craftingManager.LeaveSlot(direction);
    }

    public void MouseDown()
    {
        CheckTutorialSteps(false);
        Drag();
    }

    public void MouseUp()
    {
        CheckTutorialSteps(true);
        Drop();
    }

    public abstract void Drag();
    public abstract void Drop();

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
