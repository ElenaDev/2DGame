using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public GameObject deathEnemyAnim;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
       // currentHealth = currentHealth - amount;
        currentHealth -= amount;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
            spriteRenderer.color.b, currentHealth / maxHealth);

        if(currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        deathEnemyAnim.SetActive(true);
        Destroy(this.gameObject, 0.5f);
    }

}
