namespace MVPFrogger.Presentation
{
    public sealed class NullPlayerAnimationView : IPlayerAnimationView
    {
        public static readonly NullPlayerAnimationView Instance = new NullPlayerAnimationView();

        private NullPlayerAnimationView()
        {
        }

        public void PlayIdle()
        {
        }

        public void PlayMoveForward()
        {
        }

        public void PlayMoveBackward()
        {
        }
    }
}
