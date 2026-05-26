using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class PlayerAnimatorView : MonoBehaviour, IPlayerAnimationView
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string idleTrigger = "Idle";
        [SerializeField] private string moveForwardTrigger = "MoveForward";
        [SerializeField] private string moveBackwardTrigger = "MoveBackward";

        public void PlayIdle()
        {
            SetExclusiveTrigger(idleTrigger);
        }

        public void PlayMoveForward()
        {
            SetExclusiveTrigger(moveForwardTrigger);
        }

        public void PlayMoveBackward()
        {
            SetExclusiveTrigger(moveBackwardTrigger);
        }

        private void SetExclusiveTrigger(string triggerName)
        {
            if (animator == null || string.IsNullOrWhiteSpace(triggerName))
                return;

            ResetTrigger(idleTrigger);
            ResetTrigger(moveForwardTrigger);
            ResetTrigger(moveBackwardTrigger);
            animator.SetTrigger(triggerName);
        }

        private void ResetTrigger(string triggerName)
        {
            if (!string.IsNullOrWhiteSpace(triggerName))
                animator.ResetTrigger(triggerName);
        }
    }
}
