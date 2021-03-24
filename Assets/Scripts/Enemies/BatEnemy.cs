using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public Transform[] positions;//array de posiciones para el murciélago
    public int speed;

    Vector3[] positionsVector;

    Vector3 posToGo;//posición a la que va a ir el bat
    Animator anim;
    SpriteRenderer spriteRenderer;
    float timer;
    float timeBetweenPositions;
    int currentPosIndex;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        positionsVector = new Vector3[positions.Length];//tamaño del array
        for(int i=0; i< positions.Length;i++)
        {
            positionsVector[i] = positions[i].position;
        }
        currentPosIndex = 0;
        posToGo = positions[0].position;//posición en la casilla 0 del array

        timeBetweenPositions = Random.Range(3, 6);
    }
    void Update()
    {    
        //muevo el gameobject hacia la posición posToGo
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);

        if(transform.position == posToGo)//si he llegado a mi posición de destino, estoy parado
        {
            anim.SetBool("IsFlying", false);
            timer += Time.deltaTime;//contador de tiempo
            if (timer > timeBetweenPositions)//si ha pasado el tiempo suficiente
            {
                timer = 0;//reseteo el timer para la próxima vez que lo vaya a usar
                ChangePosition();
            }
        }
        else//no estoy en mi posición de destino, por lo tanto, estoy volando :D
        {
            anim.SetBool("IsFlying", true);
        }

        Flip();
    }
    void ChangePosition()
    {
        Vector3 lastPost = positionsVector[positions.Length - 1];
        Vector3 currentPos = positionsVector[currentPosIndex];

        positionsVector[positions.Length - 1] = currentPos;
        positionsVector[currentPosIndex] = lastPost;

        currentPosIndex = Random.Range(0, positionsVector.Length-1);//casilla aleatoria
        posToGo = positionsVector[currentPosIndex];

        timeBetweenPositions = Random.Range(3, 6);
    }

    void Flip()
    {
        //si voy hacia la derecha y flipX es falso (es decir, el bat está mirando a la izquierda)
        if(posToGo.x > transform.position.x && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;//le digo que mire a la derecha
        }
        else  if (posToGo.x < transform.position.x && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;//le digo que mire a la derecha
        }
    }
}
