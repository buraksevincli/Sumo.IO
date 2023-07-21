using System;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class FoodController : MonoBehaviour
    {
        [SerializeField] private GameObject visualObject;
        [SerializeField] private Collider collider;
        
        private Action<FoodController> _onComplete;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ITrigger iTrigger))
            {
                iTrigger.OnTrigger();
                visualObject.SetActive(false);
                collider.enabled = false;
                _onComplete?.Invoke(this);
            }
        }

        public void Initialize(Vector3 position, Action<FoodController> onComplete)
        {
            transform.localPosition = position;
            visualObject.SetActive(true);
            collider.enabled = true;
            _onComplete = onComplete;
        }
        
    }
}
