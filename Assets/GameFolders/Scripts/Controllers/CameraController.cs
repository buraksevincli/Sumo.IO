using System;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UpdateType updateType;
        [SerializeField] private float followSpeed;

        private Transform _target;
        private Vector3 _offset;
        
        private Vector3 FollowPosition => _target.position + _offset;
        private bool IsTargetNull => _target == null;

        public void AssignTarget(Transform target)
        {
            _offset = transform.position - target.position ;
            _target = target;
        }

        private void FixedUpdate()
        {
            if (updateType != UpdateType.FixedUpdate) return;

            if (IsTargetNull) return;
            
            transform.position = Vector3.Lerp(transform.position, FollowPosition, followSpeed * Time.deltaTime);
            
        }

        private void Update()
        {
            if (updateType != UpdateType.Update) return;

            if (IsTargetNull) return;
            
            transform.position = Vector3.Lerp(transform.position, FollowPosition, followSpeed * Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (updateType != UpdateType.LatestUpdate) return;
            
            if (IsTargetNull) return;
            
            transform.position = Vector3.Lerp(transform.position, FollowPosition, followSpeed * Time.deltaTime);
        }
    }
}
