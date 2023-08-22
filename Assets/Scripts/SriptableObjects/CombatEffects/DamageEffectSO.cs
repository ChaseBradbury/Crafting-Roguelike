using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CombatEffects/Damage Effect")]
public class DamageEffectSO : CombatEffectSO
{
    public int damage;

    public override void ExecuteEffect()
    {
        Damage();
    }

    public override void ContinueEffect()
    {
        Damage();
    }

    public override void EndEffect()
    {
        // End Damage
    }

    public void Damage()
    {
        entityController.GetComponent<HealthController>().Damage(damage);
    }
}
