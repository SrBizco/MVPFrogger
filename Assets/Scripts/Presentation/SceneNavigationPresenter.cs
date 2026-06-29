namespace MVPFrogger.Presentation
{
    public sealed class SceneNavigationPresenter
    {
        private readonly ISceneNavigationView view;

        public SceneNavigationPresenter(ISceneNavigationView view)
        {
            this.view = view;
        }

        public void GoToGame()
        {
            view.LoadGame();
        }

        public void GoToMainMenu()
        {
            view.LoadMainMenu();
        }
    }
}
