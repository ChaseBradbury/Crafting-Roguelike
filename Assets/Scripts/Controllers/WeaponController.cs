using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    [SerializeField] private float centerSize = 150f;
    [SerializeField] private float ringSize = 400f;
    [SerializeField] private float baseSize = 250f;
    [SerializeField] private FragmentSlotController centerFragmentSlot;
    [SerializeField] private FragmentSlotController northFragmentSlot;
    [SerializeField] private FragmentSlotController eastFragmentSlot;
    [SerializeField] private FragmentSlotController southFragmentSlot;
    [SerializeField] private FragmentSlotController westFragmentSlot;
    [SerializeField] private FragmentSlotController baseFragmentSlot;

    public SlotDirection GetClosestSlot(Vector3 mousePosition)
    {
        float distanceFromCenter = Vector2.Distance(mousePosition, centerFragmentSlot.transform.position);
        float distanceFromBase = Vector2.Distance(mousePosition, baseFragmentSlot.transform.position);
        if (distanceFromCenter < centerSize)
        {
            return SlotDirection.Center;
        }
        else if (distanceFromCenter < ringSize)
        {
            return Utils.FindDirectionQuadrant(mousePosition, centerFragmentSlot.transform.position);
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
        if (slotController != null)
        {
            slotController.AddSlotItem(item);
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
            case SlotDirection.Center:
                return centerFragmentSlot;
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
