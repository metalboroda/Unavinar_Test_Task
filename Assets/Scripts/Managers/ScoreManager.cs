using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [Header("")]
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI winScreenScoreText;

        [Header("")]
        public int score;
        [SerializeField]
        private float scoreIncreaseRate = 1;

        // Private vars
        private int _speedScore;
        private float _scoreTimer;

        // Private refs
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerMovement = PlayerMovement.Instance;
        }

        private void Update()
        {
            ScoreBySpeed();
        }

        private void ScoreBySpeed()
        {
            if (GameHandler.Instance.GameState != GameState.Started) return;

            _speedScore = Mathf.RoundToInt(_playerMovement.movementSpeed / 3);

            _scoreTimer += Time.deltaTime * _playerMovement.movementSpeed / 10;

            if (_scoreTimer >= scoreIncreaseRate)
            {
                score += RoundOff(_speedScore);

                scoreText.SetText(score.ToString());

                winScreenScoreText.SetText($"SCORE \n {score}");

                _scoreTimer = 0;

                var playerScoreFx = _playerMovement.GetComponentInChildren<ParticleSystem>();

                if (playerScoreFx != null && _playerMovement.canSpeedUp)
                    playerScoreFx.Play();
            }
        }

        public int RoundOff(int i)
        {
            return ((int)Mathf.Round(i / 10.0f)) * 10;
        }
    }
}