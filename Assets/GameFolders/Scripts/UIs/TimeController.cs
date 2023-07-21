using System;
using System.Collections;
using Sumo.Core;
using Sumo.GamePlay;
using TMPro;
using UnityEngine;

namespace Sumo.UI
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private int startTime;
        [SerializeField] private float gameTime;
        [SerializeField] private TMP_Text startTimeText; 
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject timerPanel;
        [SerializeField] private GameObject startTimerPanel;

        private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);
        
        private float _gameTimeCountdown;
        private bool _isGamePlaying;
        private int _minutes;
        private int _seconds;
        private int _currentTime;

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnGameStart += StartGameHandler;
            DataManager.Instance.EventData.OnGameEnd += EndMatch;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnGameStart -= StartGameHandler;
            DataManager.Instance.EventData.OnGameEnd -= EndMatch;
        }

        private void Start()
        {
            StartCoroutine(StartMatch());
        }

        private void Update()
        {
            if (!GameManager.Instance.Playabilty) return;

            UpdateMatchTimer();
        }

        private void StartGameHandler()
        {
            _isGamePlaying = true;
            _gameTimeCountdown = gameTime;
            timerPanel.SetActive(true);
        }

        private void UpdateMatchTimer()
        {
            if (!_isGamePlaying) return;

            _gameTimeCountdown -= Time.deltaTime;

            _minutes = Mathf.FloorToInt(_gameTimeCountdown / 60);
            _seconds = Mathf.FloorToInt(_gameTimeCountdown % 60);

            timerText.text = $"{_minutes:00}:{_seconds:00}";
            
            if (_gameTimeCountdown <= 0)
            {
                DataManager.Instance.EventData.OnGameEnd?.Invoke();
            }
        }

        private void EndMatch()
        {
            _isGamePlaying = false;
            timerPanel.SetActive(false);
            startTimerPanel.SetActive(false);
        }

        private IEnumerator StartMatch()
        {
            _currentTime = startTime;
            
            while (_currentTime > 0)
            {
                startTimeText.text = $"{_currentTime}";
                yield return _waitOneSecond;
                _currentTime--;
            }

            startTimeText.text = string.Empty;
            startTimerPanel.SetActive(false);
            DataManager.Instance.EventData.OnGameStart?.Invoke();
        }
    }
}
