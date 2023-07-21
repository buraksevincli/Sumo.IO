using System;
using System.Collections;
using Sumo.GamePlay;
using UnityEngine;

namespace Sumo.Movements
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float turnSpeed;
        [SerializeField] protected float gravityScale;
        [SerializeField] protected float maxVelocity;
        [SerializeField] private float maxAngle;
        
        protected Rigidbody rigidbody;
        protected Vector3 direction;

        protected Vector3 velocity;

        private bool _canMove;

        protected bool CanMove
        {
            get => _canMove & GameManager.Instance.Playabilty;
            set => _canMove = value;
        }
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected void Start()
        {
            CanMove = true;
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }
        
        protected virtual void Update()
        {
            direction = CalculateDirection();
            Rotate(direction);
        }

        protected void Move()
        {
            if (!CanMove) return;

             velocity = transform.forward * moveSpeed;
             velocity -= Physics.gravity * gravityScale;
             rigidbody.velocity = velocity;
        }

        protected virtual void Rotate(Vector3 direction)
        {
            if (!CanMove) return;
            
            if (Vector3.Distance(direction, Vector3.zero) < 0.1f) return;

            transform.forward = Vector3.Lerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }

        protected abstract Vector3 CalculateDirection();

        private Coroutine _knockedCoroutine;
        
        public void Knocked(Vector3 forceDirection, float damage)
        {
            if (!CanMove)
            {
                StopCoroutine(_knockedCoroutine);
                CanMove = true;
            }
            
            _knockedCoroutine = StartCoroutine(KnockedCoroutine(forceDirection, damage));
        }

        private IEnumerator KnockedCoroutine(Vector3 forceDirection, float damage)
        {
            CanMove = false;

            if (Mathf.Abs(rigidbody.velocity.x) > maxVelocity) // Limit velocity x
            {
                rigidbody.velocity = new Vector3(maxVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
            }

            if (Mathf.Abs(rigidbody.velocity.z) > maxVelocity) // Limit velocity x
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, maxVelocity);
            }

            while (rigidbody.velocity.magnitude > 0.001f)
            {
                rigidbody.velocity -= Physics.gravity * (gravityScale * 0.25f);
            
                yield return new WaitForFixedUpdate();
            }
            
            CanMove = true;
        }
    }
}
