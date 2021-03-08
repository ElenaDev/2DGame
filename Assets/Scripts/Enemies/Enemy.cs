using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    int direction = 1;//me da la dirección de movimiento
    bool hitted;//cuando está siendo golpeado
    SpriteRenderer spriteRenderer;
    GameObject player;
    Animator anim;
    Rigidbody2D rb2d;

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
        
    }
    void Update()
    {
        if(hitted == true)
        {
            return;//si está siendo golpeado, se sale del update y no ejecuta las líneas de código de abajo
        }
        //Comprobamos a que distancia está el player
        if(Vector2.Distance(transform.position, player.transform.position) < distanceToPlayer)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }             
    }
    void AttackPlayer()
    {
        if(player.transform.position.x > transform.position.x && spriteRenderer.flipX == false)//si el player está a la derecha del enemigo
        {
            spriteRenderer.flipX = true;
        }
        else if (player.transform.position.x < transform.position.x && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        SetRunning();
        Vector2 posToGo = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * 2 *
            Time.deltaTime);
    }
    void Patrol()
    {
        if (direction == 1 && spriteRenderer.flipX == false) spriteRenderer.flipX = true;
        else if (direction == -1 && spriteRenderer.flipX == true) spriteRenderer.flipX = false;

        SetWalking();
        if (transform.position.x >= limitXRight)//estoy caminanado hacia la derecha y paso el límite (por la derecha)
        {
            direction = -1;//decirle que camine hacia la izquierda
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x <= limitXLeft)
        {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        transform.Translate(direction * Vector2.right * speed * Time.deltaTime);
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
        if(collision.collider.CompareTag("Player"))//El gameobject con el que ha chocado el enemigo tiene la etiqueta de player?
        {
            int directionForce;
            if (spriteRenderer.flipX) directionForce = -1;
            else directionForce = 1;
            hitted = true;
            SetHitting();
            rb2d.AddForce(Vector2.up * forceUp);
            rb2d.AddForce(directionForce * Vector2.right * forceRight);
            //StartCoroutine(BackToIdle());
            Invoke("BackToIdle2", 2);//Llama a la función a los 2 segundos
        }
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
