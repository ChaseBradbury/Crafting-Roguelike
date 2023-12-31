using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Transform barTransform;

    public void Start()
    {
        SetMaxHealth(maxHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        UpdateSprite();
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            transform.GetComponent<EntityController>().HealthDrained();
        }
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        float ratio = ((float)currentHealth)/maxHealth;
        barTransform.localScale = new Vector3(ratio, barTransform.localScale.y, barTransform.localScale.z);
        barTransform.GetComponent<SpriteRenderer>().color = colorGradient.Evaluate(ratio);

    }
}
