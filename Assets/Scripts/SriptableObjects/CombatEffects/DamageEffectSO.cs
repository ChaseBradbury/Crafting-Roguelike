using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CombatEffects/Damage Effect")]
public class DamageEffectSO : CombatEffectSO
{
    public int damage;

    public override void ExecuteEffect()
    {
        enemyController.GetComponent<HealthController>().Damage(damage);
    }

    public override void ContinueEffect()
    {
        // Continue Damage
    }

    public override void EndEffect()
    {
        // End Damage
    }
}
