using System;
using System.Collections.Generic;
using System.Linq;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.Movements
{
    public class AIMovement : Movement, ITriggerAction
    {
        [SerializeField] private float outDistance;
        [SerializeField] private float maxTargetDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float searchTime;
        [SerializeField] private Vector2 borders;
        
        private Transform _target;
        private List<Transform> _targets = new List<Transform>();

        private float _lastSearchTime;
        
        private bool IsTargetNull => _target == null;
        private bool CanSearch => Time.time > _lastSearchTime + searchTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Food") || other.CompareTag("AI"))
            {
               _targets.Add(other.transform);
               AssignNewTarget();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Food") || other.CompareTag("AI"))
            {
                _targets.Remove(other.transform);
            }
        }

        protected override void Update()
        {
            if (!CanSearch) return;

            _lastSearchTime = Time.time;
            
            if (IsTargetNull)
            {
                AssignNewTarget();
                return;
            }

            if (Vector3.Distance(_target.position, transform.position) > outDistance)
            {
                AssignNewTarget();
            }
            
            base.Update();
        }

        private void AssignNewTarget()
        {
            _target = _targets.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList().First();
        }
        
        protected override Vector3 CalculateDirection()
        {
            Vector3 calculatingDirection = Vector3.zero;
            
            if (Mathf.Abs(transform.position.x) > borders.x || Mathf.Abs(transform.position.z) > borders.y)
            {
                calculatingDirection = Vector3.zero - transform.position;
                calculatingDirection.y = 0;
                calculatingDirection.Normalize();
                return calculatingDirection;
            }
            
            if (IsTargetNull) return calculatingDirection;

            calculatingDirection = _target.position - transform.position;
            calculatingDirection.y = 0;
            calculatingDirection.Normalize();
            return calculatingDirection;
        }

        public void Action()
        {
            AssignNewTarget();
        }
    }
}
