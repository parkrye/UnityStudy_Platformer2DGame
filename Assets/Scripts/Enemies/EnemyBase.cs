using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float speed, highSpeed, power;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    [SerializeField] protected int dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dir = -1;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag is "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TrapMove((collision.transform.position - transform.position).normalized * power, 1);
            rb.AddForce((transform.position - collision.transform.position).normalized * power, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag is "Player" && Vector2.Distance(transform.position, collision.transform.position) < 1f)
        {
            collision.GetComponent<PlayerController>().TrapMove((collision.transform.position - transform.position).normalized * power, 1);
            rb.AddForce((transform.position - collision.transform.position).normalized * power, ForceMode2D.Impulse);
        }
    }

    protected virtual void Update()
    {
        CheckMove();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if(rb.velocity.x * dir < highSpeed)
            rb.AddForce(transform.right * dir * speed, ForceMode2D.Force);
    }

    protected virtual void CheckMove()
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

    public virtual void Die()
    {
        anim.SetTrigger("Die");
        rb.gravityScale = 1f;
        gameObject.layer = LayerMask.GetMask("Ground");
        Destroy(this);
    }
}
