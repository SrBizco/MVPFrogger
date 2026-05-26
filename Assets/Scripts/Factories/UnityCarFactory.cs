using MVPFrogger.Configuration;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Factories
{
    public sealed class UnityCarFactory : ICarFactory
    {
        private readonly GameObject[] carPrefabs;
        private readonly Transform spawnPoint;
        private readonly Transform carsParent;
        private readonly CarMovementConfig movementConfig;

        public UnityCarFactory(
            GameObject[] carPrefabs,
            Transform spawnPoint,
            Transform carsParent,
            CarMovementConfig movementConfig)
        {
            this.carPrefabs = carPrefabs;
            this.spawnPoint = spawnPoint;
            this.carsParent = carsParent;
            this.movementConfig = movementConfig;
        }

        public void CreateCar()
        {
            GameObject selectedPrefab = GetRandomPrefab();

            if (selectedPrefab == null || spawnPoint == null)
            {
                return;
            }

            GameObject car = Object.Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation, carsParent);
            ICarMovementConfigReceiver configReceiver = car.GetComponent<ICarMovementConfigReceiver>();
            configReceiver?.Configure(movementConfig);
        }

        private GameObject GetRandomPrefab()
        {
            if (carPrefabs == null || carPrefabs.Length == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, carPrefabs.Length);
            return carPrefabs[randomIndex];
        }
    }
}
