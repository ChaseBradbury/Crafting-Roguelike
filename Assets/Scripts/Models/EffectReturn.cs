using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReturn
{
    public bool terminated;
    public EntityStatus effectStatus;

    public EffectReturn(){}

    public EffectReturn(EntityStatus effectStatus)
    {
        this.effectStatus = effectStatus;
    }
}
