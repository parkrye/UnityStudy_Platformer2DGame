using UnityEngine;

public class Riddle : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform[] sides;
    [SerializeField] bool onRiddle;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
        sides[2].localPosition = new Vector2(0, 0.5f * spriteRenderer.size.y);
        sides[3].localPosition = new Vector2(0, -0.5f * spriteRenderer.size.y);
    }

    void Update()
    {
        if(onRiddle)
        {
            if (playerController.transform.position.y > sides[2].position.y || playerController.transform.position.y < sides[3].position.y)
            {
                playerController.RiddleOut();
                onRiddle = false;
                return;
            }

            if (playerController.transform.position.x < sides[0].position.x)
            {
                playerController.transform.position = new Vector2(sides[0].position.x, playerController.transform.position.y);
            }
            else if (playerController.transform.position.x > sides[1].position.x)
            {
                playerController.transform.position = new Vector2(sides[1].position.x, playerController.transform.position.y);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerController = collision.GetComponent<PlayerController>();
            if(playerController.MoveDir.y != 0)
            {
                onRiddle = true;
                playerController.RiddleIn(RiddleOut);
            }
        }
    }

    void RiddleOut()
    {
        onRiddle = false;
    }
}
