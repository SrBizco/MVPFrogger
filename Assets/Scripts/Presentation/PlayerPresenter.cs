using System;
using MVPFrogger.Model;

namespace MVPFrogger.Presentation
{
    public sealed class PlayerPresenter : IDisposable
    {
        private readonly FroggerGameModel model;
        private readonly IPlayerView view;
        private readonly PlayerAnimationPresenter animationPresenter;

        public event Action PlayerChanged;

        public PlayerPresenter(
            FroggerGameModel model,
            IPlayerView view,
            PlayerAnimationPresenter animationPresenter)
        {
            this.model = model;
            this.view = view;
            this.animationPresenter = animationPresenter;

            view.ObstacleTouched += OnObstacleTouched;
            ShowCurrentLane(false);
        }

        public void Dispose()
        {
            view.ObstacleTouched -= OnObstacleTouched;
        }

        public void ShowCurrentLane(bool animated)
        {
            view.ShowLane(model.CurrentLaneIndex, animated);
        }

        private void OnObstacleTouched()
        {
            model.HitObstacle();
            animationPresenter.ShowIdle();
            ShowCurrentLane(false);
            PlayerChanged?.Invoke();
        }
    }
}
