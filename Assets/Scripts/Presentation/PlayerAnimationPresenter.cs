namespace MVPFrogger.Presentation
{
    public sealed class PlayerAnimationPresenter
    {
        private readonly IPlayerAnimationView view;

        public PlayerAnimationPresenter(IPlayerAnimationView view)
        {
            this.view = view;
        }

        public void ShowIdle()
        {
            view.PlayIdle();
        }

        public void ShowForwardMovement()
        {
            view.PlayMoveForward();
        }

        public void ShowBackwardMovement()
        {
            view.PlayMoveBackward();
        }
    }
}
