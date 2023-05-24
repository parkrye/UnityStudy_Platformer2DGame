using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] CircleCollider2D coll;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject bottom;

    [SerializeField] Vector2 moveDir;
    [SerializeField] float moveSpeed, highSpeed, jumpPower;
    [SerializeField] bool jump, controlable;

    [SerializeField] UnityEvent<int> HPEvent;
    [SerializeField] UnityEvent RiddleEvent;

    public Vector2 MoveDir { get { return moveDir; } }
    public bool Controlable { get { return controlable; } }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        HPEvent.AddListener(GameManager.Instance.Data.OnHPEvent);
        controlable = true;
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
        if(controlable)
        {
            if (animator.GetBool("OnRiddle"))
            {
                rigid.velocity = (transform.right * MoveDir.x + transform.up * moveDir.y * 2f);
            }
            else
            {
                if(rigid.velocity.x * moveDir.x < highSpeed)
                {
                    if (animator.GetBool("IsGround"))
                        rigid.AddForce(transform.right * moveDir.x * moveSpeed, ForceMode2D.Force);
                    else
                        rigid.AddForce(transform.right * moveDir.x * moveSpeed * 0.5f, ForceMode2D.Force);
                }
            }
        }
    }

    void Jump()
    {
        if (jump)
        {
            if (controlable)
            {
                if (animator.GetBool("IsGround"))
                {
                    if (animator.GetBool("OnRiddle"))
                    {
                        RiddleOut();
                        rigid.velocity = Vector2.zero;
                        rigid.AddForce(transform.right * moveDir.x * 2f, ForceMode2D.Impulse);
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
        Debug.DrawRay(transform.position - transform.up * 1.5f, -transform.up * 0.1f, Color.red);
        if(hit = Physics2D.Raycast(transform.position - transform.up * 1.5f, -transform.up, 0.1f, LayerMask.GetMask("Ground")))
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

    public void RiddleIn(UnityAction riddle)
    {
        if (!animator.GetBool("OnRiddle"))
        {
            RiddleEvent.AddListener(riddle);
            coll.isTrigger = true;
            rigid.gravityScale = 0f;
            animator.SetTrigger("RideRiddle");
            animator.SetBool("OnRiddle", true);
        }
    }

    public void RiddleOut()
    {
        if (animator.GetBool("OnRiddle"))
        {
            RiddleEvent?.Invoke();
            RiddleEvent.RemoveAllListeners();
            coll.isTrigger = false;
            rigid.gravityScale = 1f;
            animator.SetBool("OnRiddle", false);
        }
    }

    public void ClearMove()
    {
        controlable = false;
        animator.SetTrigger("Clear");
        GetComponent<PlayerInput>().enabled = false;
    }

    public void TrapMove(Vector2 dir, int damage)
    {
        if (controlable)
        {
            if (damage > 0)
            {
                HPEvent?.Invoke(-damage);
                rigid.velocity = Vector2.zero;
                StartCoroutine(DamageCoolTime());
            }
            rigid.AddForce(dir, ForceMode2D.Impulse);
        }
    }

    IEnumerator DamageCoolTime()
    {
        controlable = false;
        sprite.flipX = false;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);
        controlable = true;
        animator.SetTrigger("Recovery");
    }
}
