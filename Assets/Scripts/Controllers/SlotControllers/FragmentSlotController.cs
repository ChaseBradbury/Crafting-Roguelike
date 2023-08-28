using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSlotController : SlotController<FragmentSO>
{
    [SerializeField] private FragmentType fragmentTypeAccepted;

    public override void Drag()
    {
        // Does nothing
    }

    public override void Drop()
    {
        // craftingManager.OnMouseUpWeapon(direction);
    }

    public bool AcceptsFragment(FragmentSO fragment)
    {
        return fragmentTypeAccepted == fragment.fragmentType;
    }
}
