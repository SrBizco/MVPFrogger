using MVPFrogger.Configuration;
using MVPFrogger.Factories;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class UnityCarSpawnerView : MonoBehaviour, ICarSpawnerView
    {
        [SerializeField] private GameObject[] carPrefabs;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform carsParent;
        [SerializeField] private CarMovementConfig carMovement = new CarMovementConfig();

        private ICarFactory carFactory;

        private void Awake()
        {
            carFactory = new UnityCarFactory(carPrefabs, ResolveSpawnPoint(), carsParent, carMovement);
        }

        public void SpawnCar()
        {
            carFactory.CreateCar();
        }

        private Transform ResolveSpawnPoint()
        {
            if (IsSpawnPoint(spawnPoint))
            {
                return spawnPoint;
            }

            Transform[] children = GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != transform && IsSpawnPoint(children[i]))
                {
                    return children[i];
                }
            }

            return spawnPoint;
        }

        private static bool IsSpawnPoint(Transform candidate)
        {
            return candidate != null && candidate.name.Contains("SpawnPoint");
        }
    }
}
