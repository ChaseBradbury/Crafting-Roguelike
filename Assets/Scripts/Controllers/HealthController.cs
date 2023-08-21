using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Transform barTransform;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        UpdateSprite();
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        UpdateSprite();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateSprite()
    {
        float ratio = ((float)currentHealth)/maxHealth;
        barTransform.localScale = new Vector3(ratio, barTransform.localScale.y, barTransform.localScale.z);
        barTransform.GetComponent<SpriteRenderer>().color = colorGradient.Evaluate(ratio);

    }
}
