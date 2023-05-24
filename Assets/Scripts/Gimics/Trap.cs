using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform[] sides;
    [SerializeField] float power;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
        sides[2].localPosition = new Vector2(0, 0.5f * spriteRenderer.size.y);
        sides[3].localPosition = new Vector2(0, -0.5f * spriteRenderer.size.y);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TrapMove((collision.transform.position - transform.position).normalized * power, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Use");
            collision.gameObject.GetComponent<PlayerController>().TrapMove(transform.up * power, 0);
        }
    }
}
