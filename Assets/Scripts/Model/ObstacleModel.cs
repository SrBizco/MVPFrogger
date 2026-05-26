using System;

namespace MVPFrogger.Model
{
    public sealed class ObstacleModel
    {
        private readonly float speed;
        private readonly float travelDistance;

        public ObstacleModel(float speed, float travelDistance)
        {
            if (travelDistance <= 0f)
            {
                throw new ArgumentException("Travel distance must be greater than zero.");
            }

            this.speed = speed;
            this.travelDistance = travelDistance;
        }

        public float TravelledDistance { get; private set; }
        public bool ReachedRouteEnd => TravelledDistance >= travelDistance;

        public void Advance(float deltaTime)
        {
            TravelledDistance += Math.Abs(speed) * deltaTime;
        }
    }
}
