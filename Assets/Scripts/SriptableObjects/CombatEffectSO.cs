using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEffectSO : ScriptableObject
{
    [Tooltip("Unique code for the effect.")]
    public string effectCode;
    [Tooltip("Determines the order effects will be evaluated (ascending).")]
    public int priority;
    [Tooltip("Shown above enemies affected by effect.")]
    public Sprite sprite;
    public RepeatOptions repeatOptions;
    protected int repetitions = 0;
    protected int timeSinceLastRepeat = 0;
    protected EntityController entityController;
    protected EntityStatus status = new EntityStatus();
    protected float effectStrength;
    public float EffectStrength { get => effectStrength; set => effectStrength = value; }

    public EffectReturn Execute(EntityController entityController, float strength)
    {
        EffectReturn effectReturn = new EffectReturn();
        this.entityController = entityController;
        EffectStrength = strength;
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
        effectReturn.effectStatus = status;
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
        effectReturn.effectStatus = status;
        return effectReturn;
    }

    public EntityStatus EffectStatus()
    {
        CheckEffect();
        return status;
    }

    public abstract void ExecuteEffect();
    public abstract void ContinueEffect();
    public abstract void EndEffect();
    public void CheckEffect(){}
}
