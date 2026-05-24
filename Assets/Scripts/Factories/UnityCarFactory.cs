using MVPFrogger.Configuration;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Factories
{
    public sealed class UnityCarFactory : ICarFactory
    {
        private readonly GameObject carPrefab;
        private readonly Transform spawnPoint;
        private readonly Transform carsParent;
        private readonly CarMovementConfig movementConfig;

        public UnityCarFactory(
            GameObject carPrefab,
            Transform spawnPoint,
            Transform carsParent,
            CarMovementConfig movementConfig)
        {
            this.carPrefab = carPrefab;
            this.spawnPoint = spawnPoint;
            this.carsParent = carsParent;
            this.movementConfig = movementConfig;
        }

        public void CreateCar()
        {
            if (carPrefab == null || spawnPoint == null)
            {
                return;
            }

            GameObject car = Object.Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation, carsParent);
            ICarMovementConfigReceiver configReceiver = car.GetComponent<ICarMovementConfigReceiver>();
            configReceiver?.Configure(movementConfig);
        }
    }
}
