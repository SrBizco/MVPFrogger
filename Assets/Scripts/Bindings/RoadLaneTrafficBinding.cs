using MVPFrogger.Configuration;
using MVPFrogger.Factories;
using MVPFrogger.Model;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Bindings
{
    public sealed class RoadLaneTrafficBinding : MonoBehaviour, ICarSpawnerView
    {
        [Header("Spawn")]
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform carsParent;
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private float initialDelay;

        [Header("Car Movement")]
        [SerializeField] private CarMovementConfig carMovement = new CarMovementConfig();

        private CarSpawnerPresenter presenter;
        private ICarFactory carFactory;

        private void Awake()
        {
            CarSpawnerModel model = new CarSpawnerModel(spawnInterval, initialDelay);
            carFactory = new UnityCarFactory(carPrefab, spawnPoint, carsParent, carMovement);
            presenter = new CarSpawnerPresenter(model, this);
        }

        private void Update()
        {
            presenter.Tick(Time.deltaTime);
        }

        public void SpawnCar()
        {
            carFactory.CreateCar();
        }
    }
}
