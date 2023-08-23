using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CombatEffects/Random Movement Effect")]
public class RandomMovementEffectSO : CombatEffectSO
{
    public float range;
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
        Vector2 position = entityController.transform.position;
        float x = Random.Range(position.x + range, position.x - range);
        float y = Random.Range(position.y + range, position.y - range);
        status.hijackMovement = true;
        status.moveTo = new Vector2(x, y);
    }
}
