using System;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform[] lanePoints;
        [SerializeField] private LayerMask obstacleLayers;

        public event Action ObstacleTouched;

        private Transform Target => target != null ? target : transform;

        public void ShowLane(int laneIndex)
        {
            if (lanePoints == null || laneIndex < 0 || laneIndex >= lanePoints.Length || lanePoints[laneIndex] == null)
            {
                return;
            }

            Target.position = lanePoints[laneIndex].position;
        }

        private void OnTriggerEnter(Collider other)
        {
            int otherLayerMask = 1 << other.gameObject.layer;
            if ((obstacleLayers.value & otherLayerMask) != 0)
            {
                ObstacleTouched?.Invoke();
            }
        }
    }
}
