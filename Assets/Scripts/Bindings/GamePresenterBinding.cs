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
        [SerializeField] private UnitySceneNavigationView sceneNavigationView;

        private FroggerGamePresenter presenter;

        private void Awake()
        {
            FroggerGameModel model = new FroggerGameModel(totalLanes);
            IPlayerAnimationView animationView = playerAnimatorView != null
                ? playerAnimatorView
                : NullPlayerAnimationView.Instance;
            ISceneNavigationView navigationView = sceneNavigationView != null
                ? sceneNavigationView
                : NullSceneNavigationView.Instance;

            presenter = new FroggerGamePresenter(model, inputView, playerView, hudView, animationView, navigationView);
        }

        private void OnDestroy()
        {
            presenter?.Dispose();
        }
    }
}
