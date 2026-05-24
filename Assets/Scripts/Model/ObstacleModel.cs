using System;

namespace MVPFrogger.Model
{
    public sealed class ObstacleModel
    {
        private readonly float minX;
        private readonly float maxX;
        private readonly float speed;
        private readonly bool wrapAround;

        public ObstacleModel(float initialX, float minX, float maxX, float speed, bool wrapAround)
        {
            if (minX >= maxX)
            {
                throw new ArgumentException("minX must be lower than maxX.");
            }

            PositionX = initialX;
            this.minX = minX;
            this.maxX = maxX;
            this.speed = speed;
            this.wrapAround = wrapAround;
        }

        public float PositionX { get; private set; }

        public void Advance(float deltaTime)
        {
            PositionX += speed * deltaTime;

            if (!wrapAround)
            {
                return;
            }

            if (speed > 0f && PositionX > maxX)
            {
                PositionX = minX;
            }
            else if (speed < 0f && PositionX < minX)
            {
                PositionX = maxX;
            }
        }
    }
}
