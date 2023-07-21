using Sumo.GamePlay;
using UnityEngine;

namespace Sumo.Movements
{
    public class PlayerMovement : Movement
    {
        private float _horizontal;
        private float _vertical;
        private Vector3 _direction;

        protected override void Update()
        {
            _horizontal = JoystickInput.Instance.GetHorizontal();
            _vertical = JoystickInput.Instance.GetVertical();
            base.Update();
        }

        protected override Vector3 CalculateDirection()
        {
            if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.05f)
            {
                _direction = new Vector3(_horizontal, 0f, _vertical);
            }

            return _direction;
        }
    }
}
