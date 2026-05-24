using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class CarSpawnerPresenter
    {
        private readonly CarSpawnerModel model;
        private readonly ICarSpawnerView view;

        public CarSpawnerPresenter(CarSpawnerModel model, ICarSpawnerView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Tick(float deltaTime)
        {
            if (model.Advance(deltaTime))
            {
                view.SpawnCar();
            }
        }
    }
}
