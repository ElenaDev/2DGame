using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Life")]
    public float maxHealth;//vida máxima
    public float currentHealth; //vida actual
    public float melonLife;//vida que le vamos a añadir cuando cogemos melones
    [Header("UI")]
    public Image lifeUI;
    public float factor;
    [Header("Death")]
    public float forceJump;
    public bool isDead;//si está muerto

    bool damaged;//si ha recibido daño
    float currentFillAmount;

    void Start()
    {
        currentHealth = maxHealth;
        currentFillAmount = 1;
    }

    void Update()
    {
        if(lifeUI.fillAmount>= currentFillAmount)
        {
            lifeUI.fillAmount -= Time.deltaTime * factor;
        }
        else if(lifeUI.fillAmount <= currentFillAmount)
        {
            lifeUI.fillAmount += Time.deltaTime * factor;
        }
    }
    public void TakeDamage(int amount)
    {
        //currentHealth = currentHealth - amount;
        currentHealth -= amount;
        currentFillAmount = currentHealth / maxHealth;
        // lifeUI.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MelonLife"))
        {
            if(currentHealth == maxHealth)
            {
                return;
            }
            currentHealth += melonLife;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            currentFillAmount = currentHealth / maxHealth;
            Destroy(collision.gameObject);
        }
    }
    void Death()
    {
        isDead = true;
        GetComponent<Player2DController>().enabled = false;//el jugador ya no puede controlar al player
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceJump);
    }
}
