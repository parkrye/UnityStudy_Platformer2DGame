using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] int life, bullet;
    public int Life { get { return life; } set {  life = value; } }
    public int Bullet { get { return bullet; } set { bullet = value; } }

    void Awake()
    {
        Reset();
    }

    public void OnHPEvent(int modifier)
    {
        Life = modifier;
        if(Life <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void OnBulletEvent(int modifier)
    {
        Bullet = modifier;
    }

    public void Reset()
    {
        Life = 3;
        bullet = 5;
    }
}
