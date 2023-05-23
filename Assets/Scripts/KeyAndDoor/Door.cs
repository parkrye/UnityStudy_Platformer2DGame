using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] BoxCollider2D colldier;
    [SerializeField] GameObject key, closed, opened;
    [SerializeField] GameObject[] keys;
    [SerializeField] int keyNum;

    private void Awake()
    {
        colldier = GetComponent<BoxCollider2D>();
        for (int i = 0; i < keys.Length; i++)
        {
            if (i == keyNum)
                keys[i].SetActive(true);
            else
                keys[i].SetActive(false);
        }

        closed.SetActive(true);
        opened.SetActive(false);

    }

    public void GetKey(int num)
    {
        if(keyNum == num)
            StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.5f);
        key.SetActive(false);
        yield return new WaitForSeconds(1);
        closed.SetActive(false);
        opened.SetActive(true);
        colldier.enabled = false;
    }
}
