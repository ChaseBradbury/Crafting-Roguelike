using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    protected HealthController healthController;
    protected List<CombatEffectSO> continuousEffects;
    
    public void InitializeEntity()
    {
        
        healthController = transform.GetComponent<HealthController>();
        continuousEffects = new List<CombatEffectSO>();
    }

    public void DoContinuousEffects()
    {
        for (int i = 0; i < continuousEffects.Count; ++i)
        {
            if (continuousEffects[i].Continue().terminated)
            {
                continuousEffects.RemoveAt(i--);
            }
        }
    }

    public void AddEffect(CombatEffectSO effect)
    {
        CombatEffectSO clone = ScriptableObject.Instantiate(effect);
        if (!clone.Execute(this).terminated)
        {
            continuousEffects.Add(clone);
        }
    }

    public abstract void HealthDrained();
}
