using System.Collections;
using UnityEngine;

public class DownablePlatform : MonoBehaviour
{
    [SerializeField] PlatformEffector2D effector;

    void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    public void DownJump()
    {
        effector.rotationalOffset = 180f;
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset = 0f;
    }
}
