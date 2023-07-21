using UnityEngine;

namespace Sumo.Data
{
    [CreateAssetMenu(fileName = "HitData", menuName = "Data/HitData")]
    public class HitData : ScriptableObject
    {
        [SerializeField] private float startPower;
        [SerializeField] private float powerIncreaseCoefficient;
        [SerializeField] private float scaleIncreaseCoefficient;
        
        [SerializeField] private float sideBackMultiplier;
        [SerializeField] private float frontMultiplier;
        [SerializeField] private float backMultiplier;
        [SerializeField] private float frontDefMultiplier;
        
        public float StartPower => startPower;
        public float PowerIncreaseCoefficient => powerIncreaseCoefficient;
        public float ScaleIncreaseCoefficient => scaleIncreaseCoefficient;
        public float SideBackMultiplier => sideBackMultiplier;
        public float FrontMultiplier => frontMultiplier;
        public float BackMultiplier => backMultiplier;
        public float FrontDefMultiplier => frontDefMultiplier;
    }
}
