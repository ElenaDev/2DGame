using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int direction;//esto va a valer 1 o -1
    private void Start()
    {
        Destroy(gameObject, 2);//se destruye en 5 segundos
    }

    void Update()
    {
        transform.Translate(direction * Vector2.right * speed * Time.deltaTime);
    }
}
