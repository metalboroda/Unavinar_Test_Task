using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameHandler : MonoBehaviour
    {
        public static GameHandler Instance;

        public event Action<GameState> OnGameStateChange;

        public GameState GameState;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateGameState(GameState.Start);
        }

        public void UpdateGameState(GameState newState)
        {
            GameState = newState;

            switch (newState)
            {
                case GameState.Start:
                    break;
                case GameState.Started:
                    break;
                case GameState.Win:
                    break;
                default:
                    break;
            }

            OnGameStateChange?.Invoke(newState);
        }

        public void StarGame()
        {
            UpdateGameState(GameState.Started);
        }
    }
}

public enum GameState
{
    Start,
    Started,
    Win
}