using System;
using Sumo.GamePlay;
using UnityEngine;

namespace Sumo.Player
{
    public class ArrowController : MonoBehaviour
    {
        private float _horizontal;
        private float _vertical;
        private Vector3 _direction;
        
        private void Update()
        {
            if (!GameManager.Instance.Playabilty) return;
                
            _horizontal = JoystickInput.Instance.GetHorizontal();
            _vertical = JoystickInput.Instance.GetVertical();

            if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.05f)
            {
                _direction = new Vector3(_horizontal, 0f, _vertical);
            }

            if (_direction == Vector3.zero) return;
            
            transform.forward = _direction;
        }
    }
}
