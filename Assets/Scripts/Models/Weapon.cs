using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField] private CenterFragmentSO centerFragment;
    [SerializeField] private RingFragmentSO[] ringFragments = new RingFragmentSO[4];
    [SerializeField] private BaseFragmentSO baseFragment;

    public CenterFragmentSO CenterFragment { get => centerFragment; set => centerFragment = value; }
    public RingFragmentSO[] RingFragments { get => ringFragments; set => ringFragments = value; }
    public BaseFragmentSO BaseFragment { get => baseFragment; set => baseFragment = value; }

    public void UpdateCenter(CenterFragmentSO fragment)
    {
        if (fragment.fragmentType == FragmentType.Center)
        {
            CenterFragment = fragment;
        }
    }

    public void UpdateRing(RingFragmentSO fragment, SlotDirection direction)
    {
        if (fragment.fragmentType == FragmentType.Ring)
        {
            switch (direction)
            {
                case SlotDirection.North:
                    RingFragments[0] = fragment;
                    break;
                case SlotDirection.East:
                    RingFragments[1] = fragment;
                    break;
                case SlotDirection.South:
                    RingFragments[2] = fragment;
                    break;
                case SlotDirection.West:
                    RingFragments[3] = fragment;
                    break;
            }
        }
    }

    public void UpdateBase(BaseFragmentSO fragment)
    {
        if (fragment.fragmentType == FragmentType.Base)
        {
            BaseFragment = fragment;
        }
    }

    public void UpdateFragment(FragmentSO fragment, SlotDirection direction)
    {
        switch(direction)
        {
            case SlotDirection.Base:
                UpdateBase(fragment as BaseFragmentSO);
                break;
            case SlotDirection.Center:
                UpdateCenter(fragment as CenterFragmentSO);
                break;
            case SlotDirection.North:
            case SlotDirection.East:
            case SlotDirection.South:
            case SlotDirection.West:
                UpdateRing(fragment as RingFragmentSO, direction);
                break;
        }
    }
}
