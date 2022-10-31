using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerControls : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField]
        private float rotationSpeed = 10f;
        [SerializeField]
        private Transform model;

        // Private vars
        private float xAxis;

        private void Update()
        {
            GetTouchAxis();
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (GameHandler.Instance.GameState == GameState.Started)
            {
                model.transform.Rotate(Vector3.up, xAxis * rotationSpeed * Time.deltaTime);
            }
        }

        private void GetTouchAxis()
        {
            xAxis = UltimateTouchpad.GetHorizontalAxis("Touchpad");
        }
    }
}