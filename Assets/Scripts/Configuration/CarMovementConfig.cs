using System;

namespace MVPFrogger.Configuration
{
    [Serializable]
    public sealed class CarMovementConfig
    {
        public float Speed = 3f;
        public float TravelDistance = 18f;
        public bool DestroyWhenRouteEnds = true;

        public CarMovementConfig()
        {
        }

        public CarMovementConfig(float speed, float travelDistance, bool destroyWhenRouteEnds)
        {
            Speed = speed;
            TravelDistance = travelDistance;
            DestroyWhenRouteEnds = destroyWhenRouteEnds;
        }
    }
}
