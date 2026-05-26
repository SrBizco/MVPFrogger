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

            presenter = new MainMenuPresenter(menuView, navigationView, quitView);
        }

        private void OnDestroy()
        {
            presenter?.Dispose();
        }
    }
}
