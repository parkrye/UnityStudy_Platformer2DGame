using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] UnityEvent<int> HPModifiedEvent;

    void Awake()
    {
        life = 3;
    }

    private void Start()
    {
        HPModifiedEvent?.Invoke(life);
    }

    public void OnHPEvent(int modifier)
    {
        life += modifier;
        HPModifiedEvent?.Invoke(life);
    }
}
