using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int direction = 1;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direction * Vector2.right * speed * Time.deltaTime);
    }
}
