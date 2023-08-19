using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField] private FragmentSO centerFragment;
    [SerializeField] private FragmentSO[] ringFragments = new FragmentSO[4];
    [SerializeField] private FragmentSO baseFragment;

    public FragmentSO CenterFragment { get => centerFragment; set => centerFragment = value; }
    public FragmentSO[] RingFragments { get => ringFragments; set => ringFragments = value; }
    public FragmentSO BaseFragment { get => baseFragment; set => baseFragment = value; }

    public void UpdateCenter(FragmentSO fragment)
    {
        if (CenterFragment.fragmentType == FragmentType.Center)
        {
            CenterFragment = fragment;
        }
    }

    public void UpdateRing(FragmentSO fragment, SlotDirection direction)
    {
        if (CenterFragment.fragmentType == FragmentType.Ring)
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

    public void UpdateBase(FragmentSO fragment)
    {
        if (CenterFragment.fragmentType == FragmentType.Base)
        {
            CenterFragment = fragment;
        }
    }
}
