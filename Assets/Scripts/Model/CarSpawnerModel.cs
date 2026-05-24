using System;

namespace MVPFrogger.Model
{
    public sealed class CarSpawnerModel
    {
        private readonly float spawnInterval;
        private float elapsedTime;

        public CarSpawnerModel(float spawnInterval, float initialDelay)
        {
            if (spawnInterval <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(spawnInterval), "Spawn interval must be greater than zero.");
            }

            this.spawnInterval = spawnInterval;
            elapsedTime = -initialDelay;
        }

        public bool Advance(float deltaTime)
        {
            elapsedTime += deltaTime;

            if (elapsedTime < spawnInterval)
            {
                return false;
            }

            elapsedTime = 0f;
            return true;
        }
    }
}
