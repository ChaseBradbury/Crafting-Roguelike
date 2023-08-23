using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EntityController : MonoBehaviour
{
    protected HealthController healthController;
    protected Dictionary<string, CombatEffectSO> continuousEffects;
    protected EntityStatus status;
    
    public void InitializeEntity()
    {
        status = new EntityStatus();
        healthController = transform.GetComponent<HealthController>();
        continuousEffects = new Dictionary<string, CombatEffectSO>();
    }

    public void DoContinuousEffects()
    {
        foreach (KeyValuePair<string, CombatEffectSO> entry in continuousEffects.OrderBy(entry => entry.Value.priority))
        {
            EffectReturn effectReturn = entry.Value.Continue();
            EvaluateStatus(effectReturn.effectStatus);
            if (effectReturn.terminated)
            {
                continuousEffects.Remove(entry.Key);
            }
        }
    }

    public void AddEffect(CombatEffectSO effect)
    {
        CombatEffectSO clone = ScriptableObject.Instantiate(effect);
        EffectReturn effectReturn = clone.Execute(this);
        EvaluateStatus(effectReturn.effectStatus);
        if (!effectReturn.terminated)
        {
            continuousEffects[clone.effectCode] = clone;
        }
    }

    public void EvaluateStatus(EntityStatus statusToEvaluate)
    {
        if (statusToEvaluate.lockMovement)
        {
            status.lockMovement = true;
        }
        if (statusToEvaluate.hijackMovement)
        {
            status.moveTo = statusToEvaluate.moveTo;
        }
    }

    public abstract void HealthDrained();
}
