using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CombatEffects/Lock Movement Effect")]
public class LockMovementEffectSO : CombatEffectSO
{
    public override void ContinueEffect()
    {
        status.lockMovement = true;
    }

    public override void EndEffect()
    {
        status.lockMovement = false;
    }

    public override void ExecuteEffect()
    {
        status.lockMovement = true;
    }
}
