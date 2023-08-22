using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEffectSO : ScriptableObject
{
    public RepeatOptions repeatOptions;
    protected int repetitions = 0;
    protected int timeSinceLastRepeat = 0;
    protected EntityController entityController;
    protected EffectStatus status;

    public EffectReturn Execute(EntityController entityController)
    {
        EffectReturn effectReturn = new EffectReturn();
        this.entityController = entityController;
        ExecuteEffect();
        if (repeatOptions.repeatable)
        {
            effectReturn.terminated = false;
        }
        else
        {
            EndEffect();
            effectReturn.terminated = true;
        }
        return effectReturn;
    }

    public EffectReturn Continue()
    {
        EffectReturn effectReturn = new EffectReturn();
        effectReturn.terminated = false;
        if (timeSinceLastRepeat >= repeatOptions.repeatInterval)
        {
            timeSinceLastRepeat = 0;
            if (repetitions < repeatOptions.repetitionNumber)
            {
                ContinueEffect();
                ++repetitions;
                if (repetitions >= repeatOptions.repetitionNumber)
                {
                    EndEffect();
                    effectReturn.terminated = true;
                }
            }
        }
        else
        {
            ++timeSinceLastRepeat;
        }
        return effectReturn;
    }

    public EffectStatus EffectStatus()
    {
        CheckEffect();
        return status;
    }

    public abstract void ExecuteEffect();
    public abstract void ContinueEffect();
    public abstract void EndEffect();
    public void CheckEffect(){}
}
