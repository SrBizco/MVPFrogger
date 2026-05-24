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
                movementConfig.MinX,
                movementConfig.MaxX,
                movementConfig.Speed,
                movementConfig.DestroyWhenLeavingRoad);
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

            bool wrapAround = !movementConfig.DestroyWhenLeavingRoad;
            ObstacleModel model = new ObstacleModel(
                obstacleView.CurrentX,
                movementConfig.MinX,
                movementConfig.MaxX,
                movementConfig.Speed,
                wrapAround);
            presenter = new ObstaclePresenter(model, obstacleView);
            initialized = true;
        }

        private void Update()
        {
            Initialize();
            presenter.Tick(Time.deltaTime);

            if (movementConfig.DestroyWhenLeavingRoad && HasLeftRoad())
            {
                Destroy(gameObject);
            }
        }

        private bool HasLeftRoad()
        {
            float currentX = obstacleView.CurrentX;
            return movementConfig.Speed > 0f && currentX >= movementConfig.MaxX
                || movementConfig.Speed < 0f && currentX <= movementConfig.MinX;
        }
    }
}
