using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CombatEffects/Random Movement Effect")]
public class RandomMovementEffectSO : CombatEffectSO
{
    public MovementEffectType movementType;
    public float range;
    public float speedModifier;
    public override void ContinueEffect()
    {
        SetMovementTarget();
    }

    public override void EndEffect()
    {

    }

    public override void ExecuteEffect()
    {
        SetMovementTarget();
    }

    public void SetMovementTarget()
    {
        status.hijackMovement = true;
        switch (movementType)
        {
            case MovementEffectType.Simple:
                status.hijackMovement = false;
                break;
            case MovementEffectType.Random:
                Vector2 position = entityController.transform.position;
                float x = Random.Range(position.x + range*effectStrength, position.x - range*effectStrength);
                float y = Random.Range(position.y + range*effectStrength, position.y - range*effectStrength);
                status.moveTo = new Vector2(x, y);
                break;
            case MovementEffectType.Pull:
                status.moveTo = Vector2.MoveTowards(entityController.transform.position, effectTarget, range*effectStrength);
                break;
            case MovementEffectType.Push:
                
                status.moveTo = Vector2.MoveTowards(entityController.transform.position, effectTarget, range*effectStrength);
                break;
        }
        float modifyStrength = ((speedModifier - 1)*effectStrength) + 1;
        status.speedModifier = modifyStrength;
    }
}
