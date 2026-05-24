using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class ObstacleView : MonoBehaviour, IObstacleView
    {
        [SerializeField] private Transform target;

        public float CurrentX => Target.position.x;

        private Transform Target => target != null ? target : transform;

        public void SetX(float x)
        {
            Vector3 position = Target.position;
            position.x = x;
            Target.position = position;
        }
    }
}
