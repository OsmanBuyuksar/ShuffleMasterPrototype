using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    void OnEnable()
    {
        cam.Follow = LevelManager.Instance.mainPlayer.transform;
    }
}
