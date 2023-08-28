using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingSelectIconController : MonoBehaviour
{
    [SerializeField] private int ringIndex;
    [SerializeField] private CombatInputManager combatManager;
    private GameObject barObject;

    public void Start()
    {
        barObject = transform.Find("Bar").gameObject;
        transform.GetComponent<Image>().sprite = PlayerManager.Weapon.RingFragments[ringIndex].icon;
    }

    public void Update()
    {
        if (combatManager.WeaponRingIndex == ringIndex)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 10);
            transform.localScale = new Vector2(1, 1);
            barObject.SetActive(true);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 0);
            transform.localScale = new Vector2(.9f, .9f);
            barObject.SetActive(false);
        }
    }

    public void ClickIcon()
    {
        combatManager.UpdateWeaponMode(ringIndex);
    }
}
