using System.Collections.Generic;
using Sumo.GamePlay;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.AI
{
    public class AIController : MonoBehaviour, ITrigger
    {
        private HitController _hitController;

        private readonly List<ITriggerAction> _iTriggerActions = new List<ITriggerAction>();

        private void Awake()
        {
            _hitController = GetComponent<HitController>();
            _iTriggerActions.AddRange(GetComponents<ITriggerAction>());
            _iTriggerActions.AddRange(GetComponentsInChildren<ITriggerAction>());
        }

        public void OnTrigger()
        {
            foreach (ITriggerAction iTriggerAction in _iTriggerActions)
            {
                iTriggerAction.Action();
            }
        }
    }
}
