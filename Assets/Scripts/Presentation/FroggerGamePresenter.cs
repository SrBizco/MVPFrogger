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

        public FroggerGamePresenter(
            FroggerGameModel model,
            IGameInputView inputView,
            IPlayerView playerView,
            IGameHudView hudView)
        {
            this.model = model;
            this.inputView = inputView;
            this.playerView = playerView;
            this.hudView = hudView;

            inputView.MoveForwardRequested += OnMoveForwardRequested;
            inputView.MoveBackwardRequested += OnMoveBackwardRequested;
            inputView.RestartRequested += OnRestartRequested;
            playerView.ObstacleTouched += OnObstacleTouched;

            RefreshViews();
        }

        public void Dispose()
        {
            inputView.MoveForwardRequested -= OnMoveForwardRequested;
            inputView.MoveBackwardRequested -= OnMoveBackwardRequested;
            inputView.RestartRequested -= OnRestartRequested;
            playerView.ObstacleTouched -= OnObstacleTouched;
        }

        private void OnMoveForwardRequested()
        {
            model.MoveForward();
            RefreshViews();
        }

        private void OnMoveBackwardRequested()
        {
            model.MoveBackward();
            RefreshViews();
        }

        private void OnRestartRequested()
        {
            model.Restart();
            RefreshViews();
        }

        private void OnObstacleTouched()
        {
            model.HitObstacle();
            RefreshViews();
        }

        private void RefreshViews()
        {
            playerView.ShowLane(model.CurrentLaneIndex);

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
