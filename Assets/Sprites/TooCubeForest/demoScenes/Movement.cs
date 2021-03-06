using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Vector2 minPos;
    public Vector2 maxPos;

    private Transform moveIf;
    private Vector3 movePos;
    private Vector2 moveValue;
    private float moveSpeed = 0.04f;

    void Awake()
    {
        moveIf = transform;
        movePos = moveIf.position;
        moveValue = Vector2.one;

    }

    void FixedUpdate()
    {
        movePos.x += moveSpeed * moveValue.x;
        movePos.y += moveSpeed * moveValue.y;

        if(movePos.x < minPos.x)
        {
            moveValue.x *= -1;
            movePos.x += minPos.x - movePos.x;
        }
        else if(movePos.x > maxPos.x)
        {
            moveValue.x *= -1;
            movePos.x += maxPos.x - movePos.x;
        }


        if (movePos.y < minPos.y)
        {
            moveValue.y *= -1;
            movePos.y += minPos.y - movePos.y;
        }
        else if (movePos.y > maxPos.y)
        {
            moveValue.y *= -1;
            movePos.y += maxPos.y - movePos.y;
        }

        moveIf.position = movePos;

    }

}
