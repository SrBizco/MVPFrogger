using System;
using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class GameHudPresenter : IDisposable
    {
        private readonly FroggerGameModel model;
        private readonly IGameHudView view;
        private readonly PlayerPresenter playerPresenter;
        private readonly PlayerAnimationPresenter animationPresenter;
        private readonly SceneNavigationPresenter sceneNavigationPresenter;

        public GameHudPresenter(
            FroggerGameModel model,
            IGameHudView view,
            PlayerPresenter playerPresenter,
            PlayerAnimationPresenter animationPresenter,
            SceneNavigationPresenter sceneNavigationPresenter)
        {
            this.model = model;
            this.view = view;
            this.playerPresenter = playerPresenter;
            this.animationPresenter = animationPresenter;
            this.sceneNavigationPresenter = sceneNavigationPresenter;

            view.PlayAgainRequested += OnPlayAgainRequested;
            view.BackToMenuRequested += OnBackToMenuRequested;
            playerPresenter.PlayerChanged += Refresh;
            Refresh();
        }

        public void Dispose()
        {
            view.PlayAgainRequested -= OnPlayAgainRequested;
            view.BackToMenuRequested -= OnBackToMenuRequested;
            playerPresenter.PlayerChanged -= Refresh;
        }

        public void Refresh()
        {
            if (model.State == GameState.Won)
            {
                view.ShowWon();
                return;
            }

            view.ShowPlaying(model.CurrentLaneIndex, model.GoalLaneIndex);
        }

        private void OnPlayAgainRequested()
        {
            model.Restart();
            animationPresenter.ShowIdle();
            playerPresenter.ShowCurrentLane(false);
            Refresh();
        }

        private void OnBackToMenuRequested()
        {
            sceneNavigationPresenter.GoToMainMenu();
        }
    }
}
