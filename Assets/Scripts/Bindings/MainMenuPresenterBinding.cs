using MVPFrogger.Presentation;
using MVPFrogger.Views;
using UnityEngine;

namespace MVPFrogger.Bindings
{
    public sealed class MainMenuPresenterBinding : MonoBehaviour
    {
        [SerializeField] private MainMenuView menuView;
        [SerializeField] private UnitySceneNavigationView sceneNavigationView;
        [SerializeField] private UnityApplicationQuitView applicationQuitView;

        private MainMenuPresenter presenter;

        private void Awake()
        {
            ISceneNavigationView navigationView = sceneNavigationView != null
                ? sceneNavigationView
                : NullSceneNavigationView.Instance;
            IApplicationQuitView quitView = applicationQuitView != null
                ? applicationQuitView
                : NullApplicationQuitView.Instance;
            SceneNavigationPresenter navigationPresenter = new SceneNavigationPresenter(navigationView);
            ApplicationQuitPresenter quitPresenter = new ApplicationQuitPresenter(quitView);

            presenter = new MainMenuPresenter(menuView, navigationPresenter, quitPresenter);
        }

        private void OnDestroy()
        {
            presenter?.Dispose();
        }
    }
}
