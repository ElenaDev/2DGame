using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPro : MonoBehaviour
{
    [Header("Speed")]
    public int speed;//velocidad de movimiento
    [Header("Limits")]
    public float limitXRight;//limite de movimiento por la derecha
    public float limitXLeft;//limite de movimiento por la izquierda
    [Header("Distance to Player")]
    public float distanceToPlayer;
    [Header("ForceCollision")]
    public float forceUp;
    public float forceRight;
    [Header("Attack")]
    public int damagePerAttack;

    int direction = 1;//me da la dirección de movimiento
    int directionForce;//dirección de la fuerza en el golpeo
    bool hitted;//cuando está siendo golpeado
    SpriteRenderer spriteRenderer;
    GameObject player;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 targetPos;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");//busca un gameobject en la escena que tenga la etiqueta
        //de Player y lo guarda en la variable
    }
    private void Start()
    {
        targetPos = new Vector2(limitXRight, transform.position.y);
    }
    void Update()
    {
        if (hitted == true)
        {
            return;//si está siendo golpeado, se sale del update y no ejecuta las líneas de código de abajo
        }
        //Comprobamos a que distancia está el player
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceToPlayer)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
        Flip();
    }
    void AttackPlayer()
    {
        SetRunning();
        targetPos = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * 2 *
            Time.deltaTime);
    }
    void Patrol()
    {
        SetWalking();
        ChangeTargetPos();
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed *
        Time.deltaTime);
    }
    void Flip()
    {
        if (targetPos.x > transform.position.x && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        else if (targetPos.x < transform.position.x && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
    }
    void ChangeTargetPos()
    {
        if(Vector2.Distance(transform.position, targetPos) <= 0.1f)
        {
            if (targetPos.x == limitXRight) targetPos = new Vector2(limitXLeft, transform.position.y);
            else targetPos = new Vector2(limitXRight, transform.position.y);
        }
    }
    void SetWalking()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", true);
    }
    void SetRunning()
    {
        anim.SetBool("IsRunning", true);
        anim.SetBool("IsWalking", false);
    }
    void SetHitting()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsHitting", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))//El gameobject con el que ha chocado el enemigo tiene la etiqueta de player?
        {
            hitted = true;
            DirectionForce();//dirección de la fuerza
              
            SetHitting();//animación de daño

            AddForceGameobject(rb2d, directionForce);
            AddForceGameobject(collision.collider.GetComponent<Rigidbody2D>(), -directionForce);

           /* rb2d.AddForce(Vector2.up * forceUp);
            rb2d.AddForce(directionForce * Vector2.right * forceRight);*/
           /* collision.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceUp);
            collision.collider.GetComponent<Rigidbody2D>().AddForce(-directionForce * Vector2.right * forceRight);*/

            //StartCoroutine(BackToIdle());
            Invoke("BackToIdle2", 2);//Llama a la función a los 2 segundos

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damagePerAttack);//quitamos vida al player
            //que dios me perdone
            GetComponent<EnemyHealth>().TakeDamage(10);
        }
        else if (collision.collider.CompareTag("Duck"))
        {
            hitted = true;
            DirectionForce();//dirección de la fuerza
            SetHitting();//animación de daño
            AddForceGameobject(rb2d, directionForce/2);
            Invoke("BackToIdle2", 2);//Llama a la función a los 2 segundos
            GetComponent<EnemyHealth>().TakeDamage(collision.collider.GetComponent<ProjectileDuck>().damageDuck);
            Destroy(collision.collider.gameObject);//hasta luego pato
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ghost"))
        {
            hitted = true;
            SetHitting();
            GetComponent<EnemyHealth>().TakeDamage(collision.GetComponent<Projectile>().damageGhost);
            Destroy(collision.gameObject);//hasta luego pato
            Invoke("BackToIdle2", 2);
        }
    }
    void AddForceGameobject(Rigidbody2D _rb2d, int _direction)
    {
        _rb2d.AddForce(Vector2.up * forceUp);
        _rb2d.AddForce(_direction * Vector2.right * forceRight);
    }
    void DirectionForce()
    {      
        if (spriteRenderer.flipX) directionForce = -1;//estamos mirando el flipX del personaje para saber hacia
                                                   //donde está mirando el enemigo y darle empuje en el eje X en la dirección contraria
        else directionForce = 1;
    }
    IEnumerator BackToIdle()
    {
        yield return new WaitForSeconds(2);//se espera dos segundos para continuar leyendo el resto de líneas
        hitted = false;
        anim.SetBool("IsHitting", false);
    }
    void BackToIdle2()
    {
        hitted = false;
        anim.SetBool("IsHitting", false);
    }
}
