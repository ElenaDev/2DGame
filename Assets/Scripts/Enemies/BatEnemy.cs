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
    float timer;
    int currentPosIndex;

    void Awake()
    {
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
    }
    void Update()
    {
        timer += Time.deltaTime;//contador de tiempo
        if(timer > 3)
        {
            timer = 0;
            ChangePosition();
        }
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
    void ChangePosition()
    {
        Vector3 lastPost = positionsVector[positions.Length - 1];
        Vector3 currentPos = positionsVector[currentPosIndex];

        positionsVector[positions.Length - 1] = currentPos;
        positionsVector[currentPosIndex] = lastPost;

        currentPosIndex = Random.Range(0, positionsVector.Length-1);//casilla aleatoria
        posToGo = positionsVector[currentPosIndex];
    }
}
