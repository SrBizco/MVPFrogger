using System;
using MVPFrogger.Views;

namespace MVPFrogger.Configuration
{
    [Serializable]
    public sealed class CarSpawnerConfig
    {
        public UnityCarSpawnerView SpawnerView;
        public float SpawnInterval = 2f;
        public float InitialDelay;
    }
}
