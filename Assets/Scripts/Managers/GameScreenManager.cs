using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameScreenManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField]
        private GameObject startScreen;
        [SerializeField]
        private GameObject gameScreen;
        [SerializeField]
        private GameObject winScreen;

        private void Start()
        {
            GameHandler.Instance.OnGameStateChange += StartScreen;
            GameHandler.Instance.OnGameStateChange += GameScreen;
            GameHandler.Instance.OnGameStateChange += WinScreen;
        }

        private void OnDisable()
        {
            GameHandler.Instance.OnGameStateChange -= StartScreen;
            GameHandler.Instance.OnGameStateChange -= GameScreen;
            GameHandler.Instance.OnGameStateChange -= WinScreen;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void StartScreen(GameState state)
        {
            startScreen.SetActive(state == GameState.Start);
        }

        private void GameScreen(GameState state)
        {
            gameScreen.SetActive(state == GameState.Started);
        }

        private void WinScreen(GameState state)
        {
            if (state == GameState.Win)
            {
                StartCoroutine(WinScreenRoutine());
            }
        }

        private IEnumerator WinScreenRoutine()
        {
            yield return new WaitForSeconds(1f);

            winScreen.SetActive(true);
        }
    }
}