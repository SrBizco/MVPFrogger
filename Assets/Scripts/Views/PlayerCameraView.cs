using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class PlayerCameraView : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 playerForwardDirection = new Vector3(0f, 0f, -1f);
        [SerializeField] private float height = 10f;
        [SerializeField] private float distanceBehindPlayer = 6f;
        [SerializeField] private float lookAheadDistance = 4f;
        [SerializeField] private float followSpeed = 10f;

        private void LateUpdate()
        {
            if (player == null)
            {
                return;
            }

            Vector3 forward = playerForwardDirection.normalized;
            Vector3 targetPosition = player.position - forward * distanceBehindPlayer + Vector3.up * height;
            Vector3 lookTarget = player.position + forward * lookAheadDistance;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(lookTarget - transform.position, Vector3.up);
        }
    }
}
