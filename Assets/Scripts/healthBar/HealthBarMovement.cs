using UnityEngine;
using System.Collections;

public class HealthBarMovement : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    //SpriteRenderer spriteRenderer;
    HealthBar healthBar;
    float maxHealth;
    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponent<HealthBar>();
        maxHealth = healthBar.getMaxHealth();
    }

    public void TakeDamage(float health, float damage)
    {
        StopCoroutine(TakeDamagePrime(health, damage));
        StartCoroutine(TakeDamagePrime(health, damage));
    }

    IEnumerator TakeDamagePrime(float health, float damage)
    {
        float percentageDamage = damage / maxHealth;
        float newHealth = health - damage;
        float time = 0f;
        while (time <= 10f)
        {
            float scale =scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = healthBar.transform.localScale;
            localScale.x = (newHealth / maxHealth) + (scale * percentageDamage);
            if (localScale.x < 0)
            {
                localScale.x = 0;
            }
            healthBar.transform.localScale = localScale;

            yield return new WaitForFixedUpdate();
        }
    }
}
