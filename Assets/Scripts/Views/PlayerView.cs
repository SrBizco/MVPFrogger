using System;
using System.Collections;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform[] lanePoints;
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private float movementDuration = 0.3f;

        public event Action ObstacleTouched;

        private Coroutine movementRoutine;

        private Transform Target => target != null ? target : transform;

        public void ShowLane(int laneIndex, bool animated)
        {
            if (lanePoints == null || laneIndex < 0 || laneIndex >= lanePoints.Length || lanePoints[laneIndex] == null)
            {
                return;
            }

            Vector3 destination = lanePoints[laneIndex].position;

            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
                movementRoutine = null;
            }

            if (!animated || movementDuration <= 0f)
            {
                Target.position = destination;
                return;
            }

            movementRoutine = StartCoroutine(MoveTo(destination));
        }

        private IEnumerator MoveTo(Vector3 destination)
        {
            Vector3 origin = Target.position;
            float elapsedTime = 0f;

            while (elapsedTime < movementDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / movementDuration);
                Target.position = Vector3.Lerp(origin, destination, progress);
                yield return null;
            }

            Target.position = destination;
            movementRoutine = null;
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
