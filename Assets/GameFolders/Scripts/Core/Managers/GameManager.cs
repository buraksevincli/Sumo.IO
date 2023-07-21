using System;
using Sumo.Core;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int maxBotCount;

        private int _deadBotCount;
        
        private GameState GameState { get; set; } = GameState.Pause;

        public bool Playabilty => GameState == GameState.Play;

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnEarnScore += OnEarnScoreHandler;
            DataManager.Instance.EventData.OnGameStart += OnGameStartHandler;
            DataManager.Instance.EventData.OnGameEnd += OnGameEndHandler;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnEarnScore -= OnEarnScoreHandler;
            DataManager.Instance.EventData.OnGameStart -= OnGameStartHandler;
            DataManager.Instance.EventData.OnGameEnd -= OnGameEndHandler;
        }

        private void OnEarnScoreHandler(string obj)
        {
            _deadBotCount++;
            if (_deadBotCount >= maxBotCount)
            {
                DataManager.Instance.EventData.OnGameEnd?.Invoke();
            }
        }

        private void OnGameEndHandler()
        { 
            GameState = GameState.Pause;
        }

        private void OnGameStartHandler()
        {
            GameState = GameState.Play;
        }
    }
}
