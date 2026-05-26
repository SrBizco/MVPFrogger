using MVPFrogger.Model;
using MVPFrogger.Presentation;
using MVPFrogger.Views;
using UnityEngine;

namespace MVPFrogger.Bindings
{
    public sealed class GamePresenterBinding : MonoBehaviour
    {
        [SerializeField] private int totalLanes = 5;
        [SerializeField] private GameInputView inputView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerAnimatorView playerAnimatorView;
        [SerializeField] private GameHudView hudView;

        private FroggerGamePresenter presenter;

        private void Awake()
        {
            FroggerGameModel model = new FroggerGameModel(totalLanes);
            IPlayerAnimationView animationView = playerAnimatorView != null
                ? playerAnimatorView
                : NullPlayerAnimationView.Instance;

            presenter = new FroggerGamePresenter(model, inputView, playerView, hudView, animationView);
        }

        private void OnDestroy()
        {
            presenter?.Dispose();
        }
    }
}
