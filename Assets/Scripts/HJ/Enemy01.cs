using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Collider2D coll;
    private SpriteRenderer spriter;
    public RuntimeAnimatorController[] animCon;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public float maxHealth;
    public float health;
    public float speed;
    public Rigidbody2D target;
    WaitForFixedUpdate wait;

    bool islive;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        coll = this.gameObject.GetComponent<Collider2D>();
        anim = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        spriter = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!islive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        islive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !islive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            islive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if (GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            Dead();
        }
        IEnumerator KnockBack()
        {

            //yield return null; // 1프레임 쉬기
            //yield return new WaitForSeconds(1f); //2초 쉬기
            yield return wait;
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        }
        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
