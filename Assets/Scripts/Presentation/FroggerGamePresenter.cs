using System;
using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class FroggerGamePresenter : IDisposable
    {
        private readonly FroggerGameModel model;
        private readonly IGameInputView inputView;
        private readonly IPlayerView playerView;
        private readonly IGameHudView hudView;
        private readonly IPlayerAnimationView playerAnimationView;
        private readonly ISceneNavigationView sceneNavigationView;

        public FroggerGamePresenter(
            FroggerGameModel model,
            IGameInputView inputView,
            IPlayerView playerView,
            IGameHudView hudView,
            IPlayerAnimationView playerAnimationView,
            ISceneNavigationView sceneNavigationView)
        {
            this.model = model;
            this.inputView = inputView;
            this.playerView = playerView;
            this.hudView = hudView;
            this.playerAnimationView = playerAnimationView;
            this.sceneNavigationView = sceneNavigationView;

            inputView.MoveForwardRequested += OnMoveForwardRequested;
            inputView.MoveBackwardRequested += OnMoveBackwardRequested;
            inputView.RestartRequested += OnRestartRequested;
            playerView.ObstacleTouched += OnObstacleTouched;
            hudView.PlayAgainRequested += OnPlayAgainRequested;
            hudView.BackToMenuRequested += OnBackToMenuRequested;

            playerAnimationView.PlayIdle();
            playerView.ShowLane(model.CurrentLaneIndex, false);
            RefreshHud();
        }

        public void Dispose()
        {
            inputView.MoveForwardRequested -= OnMoveForwardRequested;
            inputView.MoveBackwardRequested -= OnMoveBackwardRequested;
            inputView.RestartRequested -= OnRestartRequested;
            playerView.ObstacleTouched -= OnObstacleTouched;
            hudView.PlayAgainRequested -= OnPlayAgainRequested;
            hudView.BackToMenuRequested -= OnBackToMenuRequested;
        }

        private void OnPlayAgainRequested()
        {
            model.Restart();
            playerAnimationView.PlayIdle();
            playerView.ShowLane(model.CurrentLaneIndex, false);
            RefreshHud();
        }

        private void OnBackToMenuRequested()
        {
            sceneNavigationView.LoadMainMenu();
        }

        private void OnMoveForwardRequested()
        {
            int previousLaneIndex = model.CurrentLaneIndex;
            model.MoveForward();

            if (model.CurrentLaneIndex != previousLaneIndex)
            {
                playerAnimationView.PlayMoveForward();
                playerView.ShowLane(model.CurrentLaneIndex, true);
            }

            RefreshHud();
        }

        private void OnMoveBackwardRequested()
        {
            int previousLaneIndex = model.CurrentLaneIndex;
            model.MoveBackward();

            if (model.CurrentLaneIndex != previousLaneIndex)
            {
                playerAnimationView.PlayMoveBackward();
                playerView.ShowLane(model.CurrentLaneIndex, true);
            }

            RefreshHud();
        }

        private void OnRestartRequested()
        {
            model.Restart();
            playerAnimationView.PlayIdle();
            playerView.ShowLane(model.CurrentLaneIndex, false);
            RefreshHud();
        }

        private void OnObstacleTouched()
        {
            model.HitObstacle();
            playerAnimationView.PlayIdle();
            playerView.ShowLane(model.CurrentLaneIndex, false);
            RefreshHud();
        }

        private void RefreshHud()
        {
            if (model.State == GameState.Won)
            {
                hudView.ShowWon();
            }
            else
            {
                hudView.ShowPlaying(model.CurrentLaneIndex, model.GoalLaneIndex);
            }
        }
    }
}
