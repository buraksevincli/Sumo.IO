using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sumo.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sumo.UI
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private UIUserItem userItemPrefab;
        [SerializeField] private RectTransform content;
        
        private List<UserScore> _usersScores = new List<UserScore>();

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnEarnScore += OnEarnScoreHandler;
            DataManager.Instance.EventData.OnGameEnd += OnGameEndHandler;
            DataManager.Instance.EventData.OnSetUser += OnSetUserHandler;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnEarnScore -= OnEarnScoreHandler;
            DataManager.Instance.EventData.OnGameEnd -= OnGameEndHandler;
            DataManager.Instance.EventData.OnSetUser -= OnSetUserHandler;
        }

        private void OnSetUserHandler(string userName)
        {
            _usersScores.Add(new UserScore()
            {
                Name = userName,
                Score = 0
            });
        }
        
        private void OnEarnScoreHandler(string userName)
        {
            UserScore userScore = _usersScores.FirstOrDefault(m => m.Name == userName);
            if (userScore != null)
            {
                userScore.Score+=100;
            }
            else
            {
                _usersScores.Add(new UserScore()
                {
                    Name = userName,
                    Score = 100
                });
            }
        }

        private void OnGameEndHandler()
        {
            content.gameObject.SetActive(true);
            
            _usersScores = _usersScores.OrderByDescending(m => m.Score).ToList();
            
            foreach (UserScore usersScore in _usersScores)
            {
                UIUserItem uiUserItem = Instantiate(userItemPrefab, content);
                uiUserItem.Initialize(usersScore.Name, usersScore.Score);
                LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            }
        }
    }

    public class UserScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}