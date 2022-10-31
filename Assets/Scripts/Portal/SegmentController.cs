using Assets.Scripts.Player;
using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Portal
{
    public class SegmentController : MonoBehaviour
    {
        public static event Action OnPlayerDetection;

        [Header("")]
        [SerializeField]
        private float cubeMoveDirection = -1f;
        [SerializeField]
        private float cubeMovementDuration = 1f;
        [SerializeField]
        private float pushPower = 10f;

        [Header("")]
        [SerializeField]
        private BoxCollider triggerCollider;
        [SerializeField]
        private BoxCollider physicsCollider;

        // Private vars
        private bool _inGroup = true;

        // Private comp
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            triggerCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _inGroup)
            {
                _inGroup = false;

                var playerMovement = other.GetComponentInParent<PlayerMovement>();

                if (playerMovement != null)
                    playerMovement.ReseSpeed();

                ChangeLayer();
                MoveCube();
                DestroyObject();

                OnPlayerDetection?.Invoke();
            }
        }

        private void ChangeLayer()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        }

        private void MoveCube()
        {
            var movePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + cubeMoveDirection);

            transform.DOMove(movePos, cubeMovementDuration).SetEase(Ease.Linear).OnComplete(EnablePhysics);
        }

        private void EnablePhysics()
        {
            rb.isKinematic = false;
            triggerCollider.enabled = false;
            physicsCollider.enabled = true;

            RandomRotation();
            Push();
        }

        private void RandomRotation()
        {
            var randNum = Random.Range(1f, 50f);

            var rot = new Vector3(transform.rotation.x + randNum, transform.rotation.y + randNum, transform.rotation.z + randNum);

            transform.DORotate(rot, 0.25f).SetEase(Ease.Linear);
        }

        private void Push()
        {
            rb.AddForce(Vector3.forward * pushPower, ForceMode.Impulse);
        }

        private void DestroyObject()
        {
            Destroy(gameObject, 5);
        }
    }
}