using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField][Range(0f, 4f)] protected float speed;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected int dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        dir = -1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TrapMove((collision.transform.position - transform.position).normalized, 1);
        }
    }

    protected void Move()
    {
        rb.velocity = (transform.right * dir * speed);
    }

    protected void Turn()
    {
        Debug.DrawRay(transform.position + transform.right * dir, -transform.up, Color.red);
        Debug.DrawRay(transform.position, transform.right * dir, Color.red);
        if (Physics2D.Raycast(transform.position + transform.right * dir, -transform.up, 1f, LayerMask.GetMask("Ground")) &&
            !Physics2D.Raycast(transform.position, transform.right * dir, 1f, LayerMask.GetMask("Ground")))
        {
            return;
        }

        dir = -dir;
        sprite.flipX = !sprite.flipX;
    }
}
