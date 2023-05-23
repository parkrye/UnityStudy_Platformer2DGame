using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int stage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        stage = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" &&
            collision.GetComponent<PlayerController>().Controlable)
        {
            anim.SetTrigger("Goal");
            collision.GetComponent<CameraController>().ChangeCamera(1);
            collision.GetComponent<PlayerController>().ClearMove();
            StartCoroutine(StageClear());
        }
    }

    IEnumerator StageClear()
    {
        yield return new WaitForSeconds(3f);
        if (stage + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(stage + 1);
        else
            SceneManager.LoadScene(0);
    }
}
