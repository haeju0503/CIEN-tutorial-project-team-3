using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    //[SerializeField]
    public Vector2 inputVec;
    public float speed;
    //public Scanner scanner;
    //public Hand[] hands;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    //private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        //scanner = this.GetComponent<Scanner>();
        //hands = this.GetComponentsInChildren<Hand>(true);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    /*
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    */

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        //anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    


}
