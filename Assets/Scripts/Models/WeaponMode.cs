using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMode
{
    private RingFragmentSO ringFragment;
    private BaseFragmentSO baseFragment;

    public WeaponMode(RingFragmentSO ringFragment, BaseFragmentSO baseFragment)
    {
        this.ringFragment = ringFragment;
        this.baseFragment = baseFragment;
    }

    public RingFragmentSO RingFragment { get => ringFragment; set => ringFragment = value; }
    public BaseFragmentSO BaseFragment { get => baseFragment; set => baseFragment = value; }
}
