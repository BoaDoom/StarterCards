using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    // SpriteRenderer spriteRenderer;
    float maxHealth = 100;
    float healthRemaining;

    public void ReduceHealth(float damage)
    {
        healthRemaining = healthRemaining - damage;
    }
    void Awake()
    {
        healthRemaining = maxHealth;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public float getHealth()
    {
        return healthRemaining;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }

}
