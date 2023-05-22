using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject bottom;
    [SerializeField] Vector2 moveDir;
    [SerializeField] float moveSpeed, jumpPower;
    [SerializeField] bool jump, damaged;
    [SerializeField] UnityEvent<int> HPEvent;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        if(!damaged)
        {
            if (animator.GetBool("OnRiddle"))
            {
                rigid.velocity = transform.up * moveSpeed * moveDir.y;
            }
            else
            {
                rigid.velocity = new Vector2(moveSpeed * moveDir.x, rigid.velocity.y);
            }
        }
    }

    void Jump()
    {
        if (jump)
        {
            if (!damaged)
            {
                if (animator.GetBool("IsGround"))
                {
                    if (animator.GetBool("OnRiddle"))
                    {
                        rigid.gravityScale = 1f;
                        animator.SetBool("OnRiddle", false);
                        rigid.AddForce(transform.up * jumpPower * 0.5f + transform.right * moveDir.x * moveSpeed, ForceMode2D.Impulse);
                    }
                    else
                    {
                        if (animator.GetBool("IsSit"))
                        {
                            if (bottom.GetComponent<DownablePlatform>())
                            {
                                bottom.GetComponent<DownablePlatform>().DownJump();
                            }
                        }
                        else
                        {
                            rigid.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                        }
                    }
                }
            }
            jump = false;
        }
    }

    void CheckGround()
    {
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(transform.position, -transform.up * 1.2f, 1.3f, LayerMask.GetMask("Ground")))
        {
            animator.SetBool("IsGround", true);
            bottom = hit.transform.gameObject;
        }
        else
        {
            animator.SetBool("IsGround", false);
        }
    }

    void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
        if(moveDir.x < 0)
        {
            sprite.flipX = true;
            animator.SetFloat("MoveSpeed", -moveDir.x);
        }
        else if(moveDir.x > 0)
        {
            sprite.flipX = false;
            animator.SetFloat("MoveSpeed", moveDir.x);
        }
        else
        {
            animator.SetFloat("MoveSpeed", moveDir.x);
        }

        if (moveDir.y < 0)
        {
            animator.SetBool("IsSit", true);
            moveDir.x = 0f;
        }
        else
        {
            animator.SetBool("IsSit", false);
        }
    }

    void OnJump()
    {
        if (!jump)
        {
            jump = true;
            animator.SetTrigger("DoJump");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Riddle" && !animator.GetBool("OnRiddle") && moveDir.y != 0)
        {
            rigid.gravityScale = 0f;
            animator.SetTrigger("RideRiddle");
            animator.SetBool("OnRiddle", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Riddle")
        {
            rigid.gravityScale = 1f;
            animator.SetBool("OnRiddle", false);
        }
    }

    public void DamageMove()
    {
        if (!damaged)
        {
            HPEvent?.Invoke(-1);
            rigid.AddForce((transform.up * 2f - transform.right) * 10f, ForceMode2D.Impulse);
            StartCoroutine(DamageCoolTime());
        }
    }

    IEnumerator DamageCoolTime()
    {
        damaged = true;
        yield return new WaitForSeconds(1f);
        damaged = false;
    }
}
