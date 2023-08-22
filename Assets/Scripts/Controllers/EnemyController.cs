using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int id;
    private EnemySO enemy;
    private HealthController healthController;
    private List<CombatEffectSO> continuousEffects;

    public int Id { get => id; set => id = value; }

    void FixedUpdate()
    {
        for (int i = 0; i < continuousEffects.Count; ++i)
        {
            if (continuousEffects[i].Continue().terminated)
            {
                continuousEffects.RemoveAt(i--);
            }
        }
    }

    public void Initialize(EnemySO enemy, Vector3 position, int id)
    {
        this.id = id;
        this.enemy = enemy;
        transform.position = position;
        //transform.Find("Sprite").localScale = new Vector3(enemy.size, enemy.size, 0);
        healthController = transform.GetComponent<HealthController>();
        healthController.SetMaxHealth(enemy.health);
        continuousEffects = new List<CombatEffectSO>();
    }

    public void AddEffect(CombatEffectSO effect)
    {
        CombatEffectSO clone = ScriptableObject.Instantiate(effect);
        if (!clone.Execute(this).terminated)
        {
            continuousEffects.Add(clone);
        }

    }
}
