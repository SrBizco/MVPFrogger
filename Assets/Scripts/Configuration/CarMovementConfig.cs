using System;

namespace MVPFrogger.Configuration
{
    [Serializable]
    public sealed class CarMovementConfig
    {
        public float MinX = -8f;
        public float MaxX = 8f;
        public float Speed = 3f;
        public bool DestroyWhenLeavingRoad = true;

        public CarMovementConfig()
        {
        }

        public CarMovementConfig(float minX, float maxX, float speed, bool destroyWhenLeavingRoad)
        {
            MinX = minX;
            MaxX = maxX;
            Speed = speed;
            DestroyWhenLeavingRoad = destroyWhenLeavingRoad;
        }
    }
}
