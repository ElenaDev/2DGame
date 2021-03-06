using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float yLimitUp;//límite superior en el eje Y, es decir, cuando llegue aquí queremos que la plataforma vaya
    //hacia abajo
    public float yLimitDown;

    [Range(1,10)]
    public int speed;

    int direction = 1;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (transform.position.y >= yLimitUp) direction = -1;
        else if (transform.position.y <= yLimitDown) direction = 1;

        transform.Translate(direction * Vector2.up * speed * Time.deltaTime);
    }
}
