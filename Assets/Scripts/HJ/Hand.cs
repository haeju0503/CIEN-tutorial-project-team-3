using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; 
    //왼손인지 확인
    public SpriteRenderer spriter;

    private SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(0.1f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);
    //위 4개의 변수들은 추후 그래픽에 따라 얼마든지 변경 될 수 있음

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }
    private void LateUpdate()
    {
        bool isReverse = player.flipX;
        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = !isReverse ? 4 : 6;
        }
    }
}
