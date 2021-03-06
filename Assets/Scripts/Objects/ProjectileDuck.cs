using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDuck : MonoBehaviour
{
    public bool right;
    public int direction;
    public float forceX;//impulso de la fuerza en el eje X
    public float forceY;//impulso de la fuerza en el eje Y
    public float forceTorque;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
       /* if (right == true)
        {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        else
        {
            direction = -1;
            spriteRenderer.flipX = false;
        }*/
        rb2d.AddForce(Vector2.up * forceY);//"darle una patada en el eje Y", añadirle fuerza
        rb2d.AddForce(direction * Vector2.right * forceX);
        rb2d.AddTorque(direction * forceTorque);
    }
    
    
}
