using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class ObstaclePresenter
    {
        private readonly ObstacleModel model;
        private readonly IObstacleView view;

        public ObstaclePresenter(ObstacleModel model, IObstacleView view)
        {
            this.model = model;
            this.view = view;
            view.SetX(model.PositionX);
        }

        public void Tick(float deltaTime)
        {
            model.Advance(deltaTime);
            view.SetX(model.PositionX);
        }
    }
}
