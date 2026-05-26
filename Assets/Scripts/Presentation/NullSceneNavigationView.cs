namespace MVPFrogger.Presentation
{
    public sealed class NullSceneNavigationView : ISceneNavigationView
    {
        public static readonly NullSceneNavigationView Instance = new NullSceneNavigationView();

        private NullSceneNavigationView()
        {
        }

        public void LoadGame()
        {
        }

        public void LoadMainMenu()
        {
        }
    }
}
