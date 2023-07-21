using System;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.Player
{
    public class PlayerController : MonoBehaviour, ITrigger
    {
        private readonly List<ITriggerAction> _iTriggerActions = new List<ITriggerAction>();

        private void Awake()
        {
            _iTriggerActions.AddRange(GetComponents<ITriggerAction>());
            _iTriggerActions.AddRange(GetComponentsInChildren<ITriggerAction>());
        }

        private void Start()
        {
            if (Camera.main != null) Camera.main.GetComponent<CameraController>().AssignTarget(transform);
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
