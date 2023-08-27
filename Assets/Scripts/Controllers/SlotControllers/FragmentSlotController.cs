using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSlotController : SlotController<FragmentSO>
{
    [SerializeField] private FragmentType fragmentTypeAccepted;

    public override void Select()
    {
        // Does nothing
    }

    public bool AcceptsFragment(FragmentSO fragment)
    {
        return fragmentTypeAccepted == fragment.fragmentType;
    }
}
