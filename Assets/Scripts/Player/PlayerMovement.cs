using Assets.Scripts.Managers;
using Assets.Scripts.Portal;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance;

        [Header("Basic Params")]
        public float movementSpeed = 5f;
        [SerializeField]
        private float pushBackDuration = 1f;
        [SerializeField]
        private GameObject movementFx;

        [Header("SpeedUp")]
        [SerializeField]
        private float speedIncrease = 1f;
        [SerializeField]
        private float maxSpeed = 10f;
        [HideInInspector]
        public bool canSpeedUp = false;

        // Hidden vars
        private bool _canMove = true;

        // Private vars
        private float _savedStartSpeed;

        // UniRX
        //private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SegmentController.OnPlayerDetection += PushBack;
            GameHandler.Instance.OnGameStateChange += MovementFx;

            _savedStartSpeed = movementSpeed;
        }

        private void Update()
        {
            MoveForward();
            SpeedUp();
        }

        private void OnDisable()
        {
            SegmentController.OnPlayerDetection -= PushBack;
            GameHandler.Instance.OnGameStateChange -= MovementFx;
        }

        private void MoveForward()
        {
            if (GameHandler.Instance.GameState == GameState.Started && _canMove)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }

        private void MovementFx(GameState state)
        {
            movementFx.SetActive(state == GameState.Started);
        }

        public void CanMoveSwitch(bool switcher)
        {
            _canMove = switcher;
        }

        public void PushBack()
        {
            CanMoveSwitch(false);

            var movePos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

            transform.DOMove(movePos, pushBackDuration).OnComplete(() =>
            {
                CanMoveSwitch(true);
            });
        }

        private void SpeedUp()
        {
            if (canSpeedUp)
            {
                if (movementSpeed < maxSpeed)
                {
                    movementSpeed += speedIncrease * Time.deltaTime;
                }
            }
        }

        public void ReseSpeed()
        {
            movementSpeed = _savedStartSpeed;

            CanSpeedUpSwitch(false);
        }

        public void CanSpeedUpSwitch(bool switcher)
        {
            canSpeedUp = switcher;
        }

        /*private void MoveForward(GameState state)
        {
            if (state == GameState.Started)
            {
                playerController.UpdatePlayerState(PlayerState.Moving);

                Observable.EveryUpdate().Subscribe(_ =>
                {
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                }).AddTo(_disposable);
            }
            else
            {
                _disposable.Clear();
            }
        }*/
    }
}