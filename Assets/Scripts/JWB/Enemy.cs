using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    bool isLive;
    private Rigidbody2D target;
    Vector2 direction;
    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
    }
}
    