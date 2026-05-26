using MVPFrogger.Configuration;
using MVPFrogger.Model;
using MVPFrogger.Presentation;
using MVPFrogger.Views;
using UnityEngine;

namespace MVPFrogger.Bindings
{
    public sealed class ObstaclePresenterBinding : MonoBehaviour, ICarMovementConfigReceiver
    {
        [SerializeField] private ObstacleView obstacleView;
        [SerializeField] private CarMovementConfig movementConfig = new CarMovementConfig();

        private ObstaclePresenter presenter;
        private bool initialized;

        public void Configure(CarMovementConfig movementConfig)
        {
            if (movementConfig == null)
            {
                return;
            }

            this.movementConfig = new CarMovementConfig(
                movementConfig.Speed,
                movementConfig.TravelDistance,
                movementConfig.DestroyWhenRouteEnds);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (initialized)
            {
                return;
            }

            ObstacleModel model = new ObstacleModel(
                movementConfig.Speed,
                movementConfig.TravelDistance);
            presenter = new ObstaclePresenter(model, obstacleView);
            initialized = true;
        }

        private void Update()
        {
            Initialize();
            presenter.Tick(Time.deltaTime);

            if (movementConfig.DestroyWhenRouteEnds && presenter.ReachedRouteEnd)
            {
                Destroy(gameObject);
            }
        }
    }
}
