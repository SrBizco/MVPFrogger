using System;

namespace MVPFrogger.Presentation
{
    public sealed class MainMenuPresenter : IDisposable
    {
        private readonly IMainMenuView menuView;
        private readonly SceneNavigationPresenter sceneNavigationPresenter;
        private readonly ApplicationQuitPresenter applicationQuitPresenter;

        public MainMenuPresenter(
            IMainMenuView menuView,
            SceneNavigationPresenter sceneNavigationPresenter,
            ApplicationQuitPresenter applicationQuitPresenter)
        {
            this.menuView = menuView;
            this.sceneNavigationPresenter = sceneNavigationPresenter;
            this.applicationQuitPresenter = applicationQuitPresenter;

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
            sceneNavigationPresenter.GoToGame();
        }

        private void OnExitRequested()
        {
            applicationQuitPresenter.Quit();
        }
    }
}
