using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] UnityEvent<int> HPModifiedEvent;


    void Start()
    {
        life = GameManager.Instance.Data.Life;
        HPModifiedEvent?.Invoke(life);
    }

    public void OnHPEvent(int modifier)
    {
        life += modifier;
        HPModifiedEvent?.Invoke(life);
    }
}
