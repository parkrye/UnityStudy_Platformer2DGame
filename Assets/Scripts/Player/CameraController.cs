using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] cameras;

    void Start()
    {
        ChangeCamera(0);
    }

    public void ChangeCamera(int num)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if(i == num)
            {
                cameras[i].Priority = 1;
            }
            else
            {
                cameras[i].Priority = 0;
            }
        }
    }
}
