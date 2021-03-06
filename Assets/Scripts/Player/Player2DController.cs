using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    [Header ("Velocity")]//para agrupar las variables públicas en el editor, para que quede bonito :)
    public float speed;
    public float acceleration;
    [Header("Raycast")]
    public Transform groundCheck;//punto de origen del raycast (los pies del player)
    public LayerMask layerMask;//la capa que va a detectar el raycast
    public float groundCheckLength;//longitud del raycast
    public bool isGrounded;//variable que vamos a usar para saber si estamos tocando el suelo
    [Header("Jump")]
    public float jumpForce;//fuerza de salto, "como de bestia va a ser la patada"
    [Header("Manager")]
    public UIManager uiManager;

    GameObject currentKiwi;//el kiwi que acabo de coger
    Vector2 targetVelocity;//velocidad a la que quiero mover al player 
    Vector3 dampVelocity;//velocidad que lleva en ese momento el player

    bool jumpPressed;//variable que me va a decir si he pulsado el botón de saltar
    bool isJumping;//variable que me va a decir si el personaje está saltando

    Rigidbody2D rg2D;
    Animator anim;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rg2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }
   
    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckLength, layerMask);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckLength, Color.red);

        float h = Input.GetAxis("Horizontal");//teclas AD
        //el ejeX del targetVelocity va a ser: h (input) * velocidad
        //el eje Y, no lo toco, le digo que siga a la velocidad que tenía en ese eje
        targetVelocity = new Vector2(h * speed , rg2D.velocity.y);
        Animating(h);//gestión del animator(máquina de estados para las animaciones)
        Flip(h);//Para dar la vuelta al personaje

        if(isGrounded && rg2D.velocity.y < 0.01f)
        {
            isJumping = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)//si el jugador pulsa la tecla espacio, y el personaje
                                                         //está tocando suelo
        {
            jumpPressed = true;
        }
        anim.SetFloat("YVelocity", rg2D.velocity.y);
        anim.SetBool("IsOnAir", !isGrounded);// ! delante de una boolena, lo que hace es coger el valor
        //contario

        /*if(Input.GetKeyDown(KeyCode.R))
        {
            currentKiwi.AddComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        }*/
    }
    private void FixedUpdate()
    {
        rg2D.velocity = Vector3.SmoothDamp(rg2D.velocity, targetVelocity, ref dampVelocity, acceleration);

        if(jumpPressed == true)
        {
            jumpPressed = false;
            isJumping = true;
            rg2D.AddForce(Vector2.up * jumpForce);
            anim.SetTrigger("Jump");
        }
    }
    void Animating(float _h)
    {
        if (_h != 0) anim.SetBool("IsRunning", true);
        else anim.SetBool("IsRunning", false);
    }
    void Flip(float _h)
    {
        if (_h > 0) spriteRenderer.flipX = false;
        else if (_h < 0) spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el gameobject con el que estoy chocando tiene la etiqueta de Platform
        if(collision.collider.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el gameobject con el que estoy chocando tiene la etiqueta de Platform
        if (collision.collider.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Kiwi"))
        {
            currentKiwi = collision.gameObject;
            currentKiwi.GetComponent<CircleCollider2D>().enabled = false;
            currentKiwi.AddComponent<Rigidbody2D>().AddForce(Vector2.up * 450);
            Invoke("AddKiwi", 0.8f);//llama a la función pasado un tiempo (en este caso 1 segundo)          
        }
    }
    void AddKiwi()
    {
        currentKiwi.GetComponent<Animator>().SetTrigger("Collected");
        uiManager.AddKiwi();
        Destroy(currentKiwi, 0.3f);
    }
}
