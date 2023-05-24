using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject[] keys;
    [SerializeField] int keyNum;
    [SerializeField] UnityEvent<int> GetKey;

    void Awake()
    {
        for(int i = 0; i < keys.Length; i++)
        {
            if(i == keyNum)
                keys[i].SetActive(true);
            else
                keys[i].SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetKey?.Invoke(keyNum);
            gameObject.SetActive(false);
        }
    }
}
