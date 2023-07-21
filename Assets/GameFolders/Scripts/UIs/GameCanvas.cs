using System;
using Sumo.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sumo.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(TryAgainButtonOnClick);
        }

        private void Start()
        {
            button.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnGameEnd += OpenTryAgainButton;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnGameEnd -= OpenTryAgainButton;
        }

        private void TryAgainButtonOnClick()
        {
            SceneManager.LoadScene(0);
        }

        private void OpenTryAgainButton()
        {
            button.gameObject.SetActive(true);
        }
    }
}
