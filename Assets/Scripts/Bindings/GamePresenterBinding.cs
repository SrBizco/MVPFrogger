using MVPFrogger.Model;
using MVPFrogger.Presentation;
using MVPFrogger.Views;
using UnityEngine;
using System;
using MVPFrogger.Configuration;

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
        [SerializeField] private CarSpawnerConfig[] trafficSpawners;

        private readonly CompositeDisposable presenters = new CompositeDisposable();
        private FroggerGameModel model;
        private PlayerPresenter playerPresenter;
        private PlayerAnimationPresenter animationPresenter;
        private GameHudPresenter hudPresenter;
        private CarSpawnerPresenter[] trafficPresenters;

        private void Awake()
        {
            model = new FroggerGameModel(totalLanes);
            IPlayerAnimationView animationView = playerAnimatorView != null
                ? playerAnimatorView
                : NullPlayerAnimationView.Instance;
            ISceneNavigationView navigationView = sceneNavigationView != null
                ? sceneNavigationView
                : NullSceneNavigationView.Instance;

            SceneNavigationPresenter navigationPresenter = new SceneNavigationPresenter(navigationView);
            animationPresenter = new PlayerAnimationPresenter(animationView);
            playerPresenter = new PlayerPresenter(model, playerView, animationPresenter);
            hudPresenter = new GameHudPresenter(
                model,
                hudView,
                playerPresenter,
                animationPresenter,
                navigationPresenter);
            GameInputPresenter inputPresenter = new GameInputPresenter(
                model,
                inputView,
                playerPresenter,
                animationPresenter,
                hudPresenter);

            animationPresenter.ShowIdle();

            presenters.Add(inputPresenter);
            presenters.Add(playerPresenter);
            presenters.Add(hudPresenter);
            CreateTrafficPresenters();
        }

        private void Update()
        {
            if (trafficPresenters == null)
            {
                return;
            }

            for (int i = 0; i < trafficPresenters.Length; i++)
            {
                trafficPresenters[i]?.Tick(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            presenters.Dispose();
        }

        private void CreateTrafficPresenters()
        {
            if (trafficSpawners == null)
            {
                trafficPresenters = new CarSpawnerPresenter[0];
                return;
            }

            trafficPresenters = new CarSpawnerPresenter[trafficSpawners.Length];

            for (int i = 0; i < trafficSpawners.Length; i++)
            {
                CarSpawnerConfig config = trafficSpawners[i];

                if (config == null || config.SpawnerView == null)
                {
                    continue;
                }

                float spawnInterval = config.SpawnInterval > 0f ? config.SpawnInterval : 2f;
                float initialDelay = config.InitialDelay > 0f ? config.InitialDelay : 0f;
                CarSpawnerModel trafficModel = new CarSpawnerModel(spawnInterval, initialDelay);
                trafficPresenters[i] = new CarSpawnerPresenter(trafficModel, config.SpawnerView);
            }
        }

        private sealed class CompositeDisposable : IDisposable
        {
            private readonly System.Collections.Generic.List<IDisposable> disposables = new System.Collections.Generic.List<IDisposable>();

            public void Add(IDisposable disposable)
            {
                if (disposable != null)
                {
                    disposables.Add(disposable);
                }
            }

            public void Dispose()
            {
                for (int i = disposables.Count - 1; i >= 0; i--)
                {
                    disposables[i].Dispose();
                }

                disposables.Clear();
            }
        }
    }
}
