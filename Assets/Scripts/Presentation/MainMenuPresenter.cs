using System;

namespace MVPFrogger.Presentation
{
    public sealed class MainMenuPresenter : IDisposable
    {
        private readonly IMainMenuView menuView;
        private readonly ISceneNavigationView sceneNavigationView;
        private readonly IApplicationQuitView applicationQuitView;

        public MainMenuPresenter(
            IMainMenuView menuView,
            ISceneNavigationView sceneNavigationView,
            IApplicationQuitView applicationQuitView)
        {
            this.menuView = menuView;
            this.sceneNavigationView = sceneNavigationView;
            this.applicationQuitView = applicationQuitView;

            menuView.StartGameRequested += OnStartGameRequested;
            menuView.ExitRequested += OnExitRequested;
        }

        public void Dispose()
        {
            menuView.StartGameRequested -= OnStartGameRequested;
            menuView.ExitRequested -= OnExitRequested;
        }

        private void OnStartGameRequested()
        {
            sceneNavigationView.LoadGame();
        }

        private void OnExitRequested()
        {
            applicationQuitView.Quit();
        }
    }
}
