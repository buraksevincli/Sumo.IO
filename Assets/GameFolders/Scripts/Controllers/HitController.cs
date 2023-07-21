using System;
using System.Collections;
using Sumo.Core;
using Sumo.Data;
using Sumo.Interface;
using Sumo.Movements;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class HitController : MonoBehaviour, ITriggerAction
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private string userName;
        [SerializeField] private HitData data;
        [SerializeField] private int tiltAngle;
        [SerializeField] private Transform visualObjectTransform;
        
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Movement _movement;
        private Vector3 _direction;
        private Vector3 _rotationAxis;
        private HitController _lastHitObject;

        private float _power;
        
        public float Power
        {
            get => _power;
            set => _power = value;
        }

        public bool OnHit;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
            _movement = GetComponent<Movement>();
        }

        private void Start()
        {
            _power = data.StartPower;
            DataManager.Instance.EventData.OnSetUser?.Invoke(userName);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("DeadGround"))
            {
                _lastHitObject.OnScore();

                if (isPlayer)
                {
                    DataManager.Instance.EventData.OnGameEnd?.Invoke();
                }
                gameObject.SetActive(false);
                return;
            }
            
            HitController otherHitController = collision.gameObject.GetComponent<HitController>();

            if (otherHitController == null) return;

            _lastHitObject = otherHitController;
            
            Vector3 myDirection = collision.transform.position - transform.position;
            Vector3 otherDirection = transform.position - collision.transform.position;
            myDirection.Normalize();
            otherDirection.Normalize();

            float damage = otherHitController.GetTrueForce(otherDirection);

            if (Vector3.Dot(myDirection, transform.forward) > 0.8f) // Çarpışmaya önümle girdim
            {
                // x1
                damage *= data.FrontDefMultiplier;
            }
            else if (Vector3.Dot(myDirection, transform.forward) < -0.8f) // Çarpışmaya arkamla giridm
            {
                damage *= data.BackMultiplier;
            }
            else // Çarpışma yanlarda 
            {
                if (Vector3.Dot(myDirection, transform.forward) <= -0.25) // Çarpışmaya arka yanımla girdim
                {
                    damage *= data.SideBackMultiplier;
                }
                else if (Vector3.Dot(myDirection, transform.forward) > -0.25) // Çarpışmaya ön yanımla girdim
                { 
                    // x1
                }
            }

            _movement.Knocked(-myDirection, damage);

            // Burada hesaplamalar sonucunda hasar alıcam
            _rigidbody.AddForce(-myDirection * damage, ForceMode.Impulse);

        }
        
        private void OnScore()
        {
            DataManager.Instance.EventData.OnEarnScore?.Invoke(userName);
            Action();
        }

        public float GetTrueForce(Vector3 direction)
        {
            float multiplierForce = _power;
            
            if (Vector3.Dot(direction, transform.forward) > 0.75f) // Çarpışmaya önümle girdim
            {
                multiplierForce *= data.FrontMultiplier;
            }

            return multiplierForce;
        }

        public void Action() // Food yedik, gücü arttır
        {
            Power += data.PowerIncreaseCoefficient;
            transform.localScale += Vector3.one * data.ScaleIncreaseCoefficient;
        }
    }
}
