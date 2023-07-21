using Sumo.Data;
using UnityEngine;

namespace Sumo.Core
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private EventData eventData;

        public EventData EventData => eventData;
    }
}
