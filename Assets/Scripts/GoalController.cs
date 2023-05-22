using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetTrigger("Goal");
        }
    }
}
