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
        }

        public bool ReachedRouteEnd => model.ReachedRouteEnd;

        public void Tick(float deltaTime)
        {
            float previousDistance = model.TravelledDistance;
            model.Advance(deltaTime);
            view.Move(model.TravelledDistance - previousDistance);
        }
    }
}
