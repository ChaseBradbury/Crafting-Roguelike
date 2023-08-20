using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMode
{
    private CenterFragmentSO centerFragment;
    private RingFragmentSO ringFragment;
    private BaseFragmentSO baseFragment;

    public WeaponMode(CenterFragmentSO centerFragment, RingFragmentSO ringFragment, BaseFragmentSO baseFragment)
    {
        this.centerFragment = centerFragment;
        this.ringFragment = ringFragment;
        this.baseFragment = baseFragment;
    }

    public CenterFragmentSO CenterFragment { get => centerFragment; set => centerFragment = value; }
    public RingFragmentSO RingFragment { get => ringFragment; set => ringFragment = value; }
    public BaseFragmentSO BaseFragment { get => baseFragment; set => baseFragment = value; }
}
