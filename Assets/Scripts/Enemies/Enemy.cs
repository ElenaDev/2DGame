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

    int direction = 1;//me da la dirección de movimiento
    SpriteRenderer spriteRenderer;
    GameObject player;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");//busca un gameobject en la escena que tenga la etiqueta
        //de Player y lo guarda en la variable
    }
    private void Start()
    {
        
    }
    void Update()
    {
        //Comprobamos a que distancia está el player
        if(Vector2.Distance(transform.position,player.transform.position) < distanceToPlayer)
        {
            Vector2 posToGo = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, posToGo, speed *
                Time.deltaTime);
        }
        else
        {
            Patrol();
        }             
    }
    void Patrol()
    {
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
}
