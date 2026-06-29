using System;
using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class GameInputPresenter : IDisposable
    {
        private readonly FroggerGameModel model;
        private readonly IGameInputView view;
        private readonly PlayerPresenter playerPresenter;
        private readonly PlayerAnimationPresenter animationPresenter;
        private readonly GameHudPresenter hudPresenter;

        public GameInputPresenter(
            FroggerGameModel model,
            IGameInputView view,
            PlayerPresenter playerPresenter,
            PlayerAnimationPresenter animationPresenter,
            GameHudPresenter hudPresenter)
        {
            this.model = model;
            this.view = view;
            this.playerPresenter = playerPresenter;
            this.animationPresenter = animationPresenter;
            this.hudPresenter = hudPresenter;

            view.MoveForwardRequested += OnMoveForwardRequested;
            view.MoveBackwardRequested += OnMoveBackwardRequested;
            view.RestartRequested += OnRestartRequested;
        }

        public void Dispose()
        {
            view.MoveForwardRequested -= OnMoveForwardRequested;
            view.MoveBackwardRequested -= OnMoveBackwardRequested;
            view.RestartRequested -= OnRestartRequested;
        }

        private void OnMoveForwardRequested()
        {
            int previousLaneIndex = model.CurrentLaneIndex;
            model.MoveForward();

            if (model.CurrentLaneIndex != previousLaneIndex)
            {
                animationPresenter.ShowForwardMovement();
                playerPresenter.ShowCurrentLane(true);
            }

            hudPresenter.Refresh();
        }

        private void OnMoveBackwardRequested()
        {
            int previousLaneIndex = model.CurrentLaneIndex;
            model.MoveBackward();

            if (model.CurrentLaneIndex != previousLaneIndex)
            {
                animationPresenter.ShowBackwardMovement();
                playerPresenter.ShowCurrentLane(true);
            }

            hudPresenter.Refresh();
        }

        private void OnRestartRequested()
        {
            model.Restart();
            animationPresenter.ShowIdle();
            playerPresenter.ShowCurrentLane(false);
            hudPresenter.Refresh();
        }
    }
}
