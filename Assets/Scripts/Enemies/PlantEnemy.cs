using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public float distanceToPlayer;//distancia a la que la planta va a atacar al player
    public Transform posLeft;//posición de salida de la bala a la izquierda
    public Transform posRight;//posición de salida de la bala a la derecha

    Vector3 positionBullet;
    int direction = 1;
    Animator anim;
    SpriteRenderer spriteRenderer;
    EnemyHealth enemyHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        //si el player está cerca
        if(Vector2.Distance(transform.position, player.position) <= distanceToPlayer)
        {
            Attack();
        }
        else
        {
            anim.SetBool("Attack", false);//dejo de atacar si el player no está cerca
        }
    }
    void Attack()
    {
        WhereIsThePlayer();
        anim.SetBool("Attack", true);//pongo a true el valor del parámetro del animator
    }
    //Esta función la vamos a usar como evento en la animación de Attack, es decir,
    //la vamos a llamar en un keyframe determinado
    public void CreateBullet()
    {
        GameObject bulletClone = Instantiate(bulletPrefab, positionBullet, new Quaternion());
        bulletClone.GetComponent<Bullet>().direction = direction;
    }
    void WhereIsThePlayer()
    {
        //si el player está a la derecha de la planta 
        if(player.position.x > transform.position.x)
        {
            direction = 1;
            positionBullet = posRight.position;
            spriteRenderer.flipX = true;
        }
        //si el player está a la izquierda de la planta 
        else if (player.position.x < transform.position.x)
        {
            direction = -1;
            positionBullet = posLeft.position;
            spriteRenderer.flipX = false;
        }
    }
    //colisión trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si ha entrado el fantasma en el collider de la planta
        if(collision.CompareTag("Ghost"))
        {
            //llamo a la función Takedamage que está en el script EnemyHealth y le paso la cantidad de vida que le 
            //vamos a quitar a la planta
            enemyHealth.TakeDamage(collision.GetComponent<Projectile>().damageGhost);
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Duck"))
        {
            enemyHealth.TakeDamage(collision.collider.GetComponent<ProjectileDuck>().damageDuck);
            Destroy(collision.gameObject);
        }
    }
}
