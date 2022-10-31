using Assets.Scripts.Player;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CinemachineController : MonoBehaviour
    {
        // Private comp
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        // Private refs
        private PlayerController playerController;

        private void Awake()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            SetCameraTarget();
        }

        private void SetCameraTarget()
        {
            if (playerController == null) return;

            cinemachineVirtualCamera.Follow = playerController.transform;
            cinemachineVirtualCamera.LookAt = playerController.transform;
        }
    }
}