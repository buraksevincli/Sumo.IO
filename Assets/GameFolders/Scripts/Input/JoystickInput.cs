using System;
using Sumo.Core;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class JoystickInput : MonoSingleton<JoystickInput>
    {
        [SerializeField] private DynamicJoystick dynamicJoystick;

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnGameStart += OnGameStartHandler;
            DataManager.Instance.EventData.OnGameEnd += OnGameEndHandler;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnGameStart -= OnGameStartHandler;
            DataManager.Instance.EventData.OnGameEnd -= OnGameEndHandler;
        }

        private void OnGameStartHandler()
        {
            dynamicJoystick.gameObject.SetActive(true);
        }
        
        private void OnGameEndHandler()
        {
            dynamicJoystick.gameObject.SetActive(false);
        }
        
        public float GetHorizontal()
        {
            return dynamicJoystick.Horizontal;
        }

        public float GetVertical()
        {
            return dynamicJoystick.Vertical;
        }

        public Vector2 GetDirection()
        {
            return dynamicJoystick.Direction;
        }
    }
}
