using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] int life, bullet;
    [SerializeField] UnityEvent<int> HPModifiedEvent, BulletModifiedEvent;


    void Start()
    {
        life = GameManager.Instance.Data.Life;
        bullet = GameManager.Instance.Data.Bullet;
        HPModifiedEvent.AddListener(GameManager.Instance.Data.OnHPEvent);
        BulletModifiedEvent.AddListener(GameManager.Instance.Data.OnBulletEvent);
        HPModifiedEvent?.Invoke(life);
        BulletModifiedEvent?.Invoke(bullet);
    }

    public void OnHPEvent(int modifier)
    {
        life += modifier;
        HPModifiedEvent?.Invoke(life);
    }

    void OnFire()
    {
        if(bullet > 0)
        {
            bullet--;
            BulletModifiedEvent?.Invoke(bullet);
        }
    }
}
