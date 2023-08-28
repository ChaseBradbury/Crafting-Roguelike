using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EntityController : MonoBehaviour
{
    protected HealthController healthController;
    protected EffectUIController effectUIController;
    protected Dictionary<string, CombatEffectSO> continuousEffects;
    protected EntityStatus status;
    
    public void InitializeEntity()
    {
        status = new EntityStatus();
        healthController = transform.GetComponent<HealthController>();
        effectUIController = transform.GetComponent<EffectUIController>();
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
                effectUIController.DeleteEffectIcon(entry.Value);
            }
        }
    }

    public void AddEffect(CombatEffectSO effect, float effectStrength, Vector2 effectTarget)
    {
        CombatEffectSO clone = ScriptableObject.Instantiate(effect);
        EffectReturn effectReturn = clone.Execute(this, effectStrength, effectTarget);
        EvaluateStatus(effectReturn.effectStatus);
        if (!continuousEffects.ContainsKey(clone.effectCode))
        {
            continuousEffects.Add(clone.effectCode, clone);
        }
        if (!effectReturn.terminated && effectStrength > continuousEffects[clone.effectCode].EffectStrength)
        {
            continuousEffects[clone.effectCode] = clone;
        }
        effectUIController.AddEffectIcon(effect, true);
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
        status.speedModifier *= statusToEvaluate.speedModifier;
    }

    public abstract void HealthDrained();
}
