using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public GameObject deathEnemyAnim;
    public float factor;

    SpriteRenderer spriteRenderer;
    float alphaValue;//el valor del canal alpha que quiero mostrar

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        alphaValue = 1;
    }

    private void Update()
    {
        if(spriteRenderer.color.a >= alphaValue)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime * factor);
        }
    }

    public void TakeDamage(int amount)
    {
       // currentHealth = currentHealth - amount;
        currentHealth -= amount;
        alphaValue = currentHealth / maxHealth;


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
