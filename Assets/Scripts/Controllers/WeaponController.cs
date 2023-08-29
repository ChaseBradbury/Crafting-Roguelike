using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    [SerializeField] private float centerSize = 150f;
    [SerializeField] private float ringSize = 400f;
    [SerializeField] private float baseSize = 250f;
    [SerializeField] private FragmentSlotController northFragmentSlot;
    [SerializeField] private FragmentSlotController eastFragmentSlot;
    [SerializeField] private FragmentSlotController southFragmentSlot;
    [SerializeField] private FragmentSlotController westFragmentSlot;
    [SerializeField] private FragmentSlotController baseFragmentSlot;
    private bool initialized = false;

    void Update()
    {
        if (!initialized && PlayerManager.Weapon != null)
        {
            AddToSlot(SlotDirection.North, PlayerManager.Weapon.RingFragments[0]);
            AddToSlot(SlotDirection.East, PlayerManager.Weapon.RingFragments[1]);
            AddToSlot(SlotDirection.South, PlayerManager.Weapon.RingFragments[2]);
            AddToSlot(SlotDirection.West, PlayerManager.Weapon.RingFragments[3]);
            AddToSlot(SlotDirection.Base, PlayerManager.Weapon.BaseFragment);
            initialized = true;
        }
    }

    public SlotDirection GetClosestSlot(Vector3 mousePosition)
    {
        float distanceFromCenter = Vector2.Distance(mousePosition, northFragmentSlot.transform.position);
        float distanceFromBase = Vector2.Distance(mousePosition, baseFragmentSlot.transform.position);
        if (distanceFromCenter < centerSize)
        {
            return SlotDirection.Center;
        }
        else if (distanceFromCenter < ringSize)
        {
            return Utils.FindDirectionQuadrant(mousePosition, northFragmentSlot.transform.position);
        }
        else if (distanceFromBase < baseSize)
        {
            return SlotDirection.Base;
        }
        {
            return SlotDirection.Null;
        }
    }

    public void AddToSlot(SlotDirection direction, FragmentSO item)
    {
        FragmentSlotController slotController = GetSlotController(direction);
        if (slotController != null && item != null)
        {
            slotController.AddSlotItem(item);
            PlayerManager.Weapon.UpdateFragment(item, direction);
        }
    }

    public bool WillAcceptItem(SlotDirection direction, ItemSO item)
    {
        if (item is FragmentSO fragment)
        {
            return GetSlotController(direction).AcceptsFragment(fragment);
        }
        return false;
    }

    public FragmentSlotController GetSlotController(SlotDirection direction)
    {
        switch (direction)
        {
            case SlotDirection.North:
                return northFragmentSlot;
            case SlotDirection.East:
                return eastFragmentSlot;
            case SlotDirection.South:
                return southFragmentSlot;
            case SlotDirection.West:
                return westFragmentSlot;
            case SlotDirection.Base:
                return baseFragmentSlot;
            default:
                return null;
                    
        }
    }
}
