using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    public RuntimeAnimatorController[] animCon;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = this.GetComponent<Scanner>();
        hands = this.GetComponentsInChildren<Hand>(true);
    }
    private void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
    
    /*
    void OnMove(InputValue value)
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec = value.Get<Vector2>();
    }
    */
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        // 키입력을 벡터에 저장
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;

        // 입력받은 벡터만큼 이동
        rigid.MovePosition(this.rigid.position + nextVec);
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= 10 * Time.deltaTime;

        if (GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
                transform.GetChild(index).gameObject.SetActive(false);
            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
