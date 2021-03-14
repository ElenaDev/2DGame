using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La cámara tiene que estar encuadrada en la escena para que en el start
/// pille bien la distancia inicial
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;//distancia inicial entre la cámara y el player
    public float smoothTargetTime = 0.25f;//velocidad con la que la cámara va a seguir al player
    Vector3 smoothDampVelocity;//voy a guardar la velocidad actual de la camara
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Start()
    {
        offset = transform.position - player.position;
    }
    
    void FixedUpdate()
    {
        if(playerHealth.isDead == true)
        {
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref smoothDampVelocity,
            smoothTargetTime);
    }
}
