using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class ObstacleView : MonoBehaviour, IObstacleView
    {
        [SerializeField] private Transform target;

        private Transform Target => target != null ? target : transform;

        public void Move(float distance)
        {
            Target.position += Target.forward * distance;
        }
    }
}
