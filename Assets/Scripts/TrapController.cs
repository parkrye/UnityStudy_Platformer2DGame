using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Vector2 dir;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TrapMove(dir, damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TrapMove(dir, damage);
        }
    }
}
