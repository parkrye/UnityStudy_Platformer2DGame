using UnityEngine;

public class SeekingArialMonster : EnemyBase
{
    [SerializeField] Vector2 initialPosition;
    enum EnemyState { Idle, Trace, Return }
    [SerializeField] EnemyState enemyState;
    [SerializeField] Transform target;

    void Awake()
    {
        initialPosition = transform.position;
        enemyState = EnemyState.Idle;
    }

    protected override void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Trace:
                break;
            case EnemyState.Return:
                CheckReturn();
                break;

        }
    }

    protected override void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                Move();
                break;
            case EnemyState.Trace:
                Trace();
                break;
            case EnemyState.Return:
                Return();
                break;

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyState == EnemyState.Idle || enemyState == EnemyState.Return)
        {
            if (collision.tag == "Player")
            {
                enemyState = EnemyState.Trace;
                target = collision.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyState == EnemyState.Trace)
        {
            if (collision.tag == "Player")
            {
                enemyState = EnemyState.Return;
                target = null;
            }
        }
    }

    void CheckReturn()
    {
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            enemyState = EnemyState.Idle;
        }
    }

    void Trace()
    {
        if ((target.position - transform.position).normalized.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;

        if (rb.velocity.x * dir < highSpeed)
            rb.AddForce(((target.position - transform.position).normalized * speed), ForceMode2D.Force);
    }

    void Return()
    {
        if (initialPosition.x - transform.position.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;

        if (rb.velocity.x * dir < highSpeed)
            rb.AddForce(((new Vector3(initialPosition.x, initialPosition.y, 0) - transform.position).normalized * speed), ForceMode2D.Force);
    }
}
