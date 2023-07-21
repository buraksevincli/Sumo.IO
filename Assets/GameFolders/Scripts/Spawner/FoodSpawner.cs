using System.Collections.Generic;
using Sumo.GamePlay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sumo.SpawnSystem
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private FoodController foodPrefab;
        [SerializeField] private Vector2 spawnArea;
        [SerializeField] private Vector2 spawnTimeRange;
        [SerializeField] private int startAmount;
        [SerializeField] private int maxFood;
        [SerializeField] private float offsetY;

        private readonly Queue<FoodController> _foods = new Queue<FoodController>();

        private float _currentTime;
        private int _spawnedFoodCount;

        private void Start()
        {
            _currentTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            Spawn(startAmount);
        }

        private void Update()
        {
            if (!GameManager.Instance.Playabilty) return;
                
            _currentTime -= Time.deltaTime;
        
            if (_currentTime > 0) return;

            Spawn();
            _currentTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }

        private void Spawn()
        {
            if (_foods.Count == 0)
            {
                if (_spawnedFoodCount >= maxFood && maxFood > 0) return;

                FoodController newFood = Instantiate(foodPrefab, transform);
                _foods.Enqueue(newFood);
                _spawnedFoodCount++;
            }

            FoodController spawnFood = _foods.Dequeue();
            float x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            float z = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            Vector3 spawnPosition = new Vector3(x, offsetY, z);

            spawnFood.Initialize(spawnPosition, ReturnToQueue);
        }

        private void Spawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_foods.Count == 0)
                {
                    if (_spawnedFoodCount >= maxFood && maxFood > 0) return;

                    FoodController newFood = Instantiate(foodPrefab, transform);
                    _foods.Enqueue(newFood);
                    _spawnedFoodCount++;
                }

                FoodController spawnFood = _foods.Dequeue();
                float x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
                float z = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
                Vector3 spawnPosition = new Vector3(x, offsetY, z);

                spawnFood.Initialize(spawnPosition, ReturnToQueue);
            }
        }

        private void ReturnToQueue(FoodController foodController)
        {
            _foods.Enqueue(foodController);
        }
    }
}