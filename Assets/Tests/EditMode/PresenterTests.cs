using System;
using MVPFrogger.Model;
using MVPFrogger.Presentation;
using NUnit.Framework;

namespace MVPFrogger.Tests.EditMode
{
    public sealed class PresenterTests
    {
        [Test]
        public void ObstaclePresenter_Tick_MovesViewByTravelledDelta()
        {
            FakeObstacleView view = new FakeObstacleView();
            ObstaclePresenter presenter = new ObstaclePresenter(new ObstacleModel(4f, 10f), view);

            presenter.Tick(0.5f);

            Assert.AreEqual(2f, view.TotalMoved);
            Assert.IsFalse(presenter.ReachedRouteEnd);
        }

        [Test]
        public void CarSpawnerPresenter_Tick_SpawnsOnlyWhenModelRequestsIt()
        {
            FakeCarSpawnerView view = new FakeCarSpawnerView();
            CarSpawnerPresenter presenter = new CarSpawnerPresenter(new CarSpawnerModel(2f, 0f), view);

            presenter.Tick(1f);
            presenter.Tick(1f);

            Assert.AreEqual(1, view.SpawnCount);
        }

        [Test]
        public void PlayerAnimationPresenter_ForwardsAnimationCommandsToView()
        {
            FakePlayerAnimationView view = new FakePlayerAnimationView();
            PlayerAnimationPresenter presenter = new PlayerAnimationPresenter(view);

            presenter.ShowIdle();
            presenter.ShowForwardMovement();
            presenter.ShowBackwardMovement();

            Assert.AreEqual(1, view.IdleCount);
            Assert.AreEqual(1, view.ForwardCount);
            Assert.AreEqual(1, view.BackwardCount);
        }

        [Test]
        public void PlayerPresenter_OnObstacleTouched_ResetsPlayerAndNotifiesChange()
        {
            FroggerGameModel model = new FroggerGameModel(4);
            model.MoveForward();
            FakePlayerView playerView = new FakePlayerView();
            FakePlayerAnimationView animationView = new FakePlayerAnimationView();
            PlayerAnimationPresenter animationPresenter = new PlayerAnimationPresenter(animationView);
            PlayerPresenter presenter = new PlayerPresenter(model, playerView, animationPresenter);
            int changeCount = 0;
            presenter.PlayerChanged += () => changeCount++;

            playerView.RaiseObstacleTouched();

            Assert.AreEqual(0, model.CurrentLaneIndex);
            Assert.AreEqual(1, animationView.IdleCount);
            Assert.AreEqual(0, playerView.LastLaneIndex);
            Assert.IsFalse(playerView.LastAnimated);
            Assert.AreEqual(1, changeCount);
        }

        [Test]
        public void GameInputPresenter_MoveForward_UpdatesModelPlayerAnimationAndHud()
        {
            PresenterContext context = new PresenterContext(4);

            context.InputView.RaiseMoveForward();

            Assert.AreEqual(1, context.Model.CurrentLaneIndex);
            Assert.AreEqual(1, context.AnimationView.ForwardCount);
            Assert.AreEqual(1, context.PlayerView.LastLaneIndex);
            Assert.IsTrue(context.PlayerView.LastAnimated);
            Assert.AreEqual(1, context.HudView.LastCurrentLaneIndex);
        }

        [Test]
        public void GameInputPresenter_MoveBackward_UpdatesModelPlayerAnimationAndHud()
        {
            PresenterContext context = new PresenterContext(4);
            context.InputView.RaiseMoveForward();

            context.InputView.RaiseMoveBackward();

            Assert.AreEqual(0, context.Model.CurrentLaneIndex);
            Assert.AreEqual(1, context.AnimationView.BackwardCount);
            Assert.AreEqual(0, context.PlayerView.LastLaneIndex);
            Assert.IsTrue(context.PlayerView.LastAnimated);
        }

        [Test]
        public void GameInputPresenter_MoveBackwardAtFirstLane_OnlyRefreshesHud()
        {
            PresenterContext context = new PresenterContext(4);
            int previousBackwardCount = context.AnimationView.BackwardCount;

            context.InputView.RaiseMoveBackward();

            Assert.AreEqual(0, context.Model.CurrentLaneIndex);
            Assert.AreEqual(previousBackwardCount, context.AnimationView.BackwardCount);
            Assert.AreEqual(0, context.PlayerView.LastLaneIndex);
            Assert.IsFalse(context.PlayerView.LastAnimated);
            Assert.GreaterOrEqual(context.HudView.PlayingCount, 2);
        }

        [Test]
        public void GameInputPresenter_Restart_ReturnsPlayerToStartAndRefreshesHud()
        {
            PresenterContext context = new PresenterContext(4);
            context.InputView.RaiseMoveForward();

            context.InputView.RaiseRestart();

            Assert.AreEqual(0, context.Model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Playing, context.Model.State);
            Assert.AreEqual(1, context.AnimationView.IdleCount);
            Assert.AreEqual(0, context.PlayerView.LastLaneIndex);
            Assert.IsFalse(context.PlayerView.LastAnimated);
        }

        [Test]
        public void GameHudPresenter_PlayAgain_RestartsModelAndShowsPlaying()
        {
            PresenterContext context = new PresenterContext(2);
            context.InputView.RaiseMoveForward();

            context.HudView.RaisePlayAgain();

            Assert.AreEqual(0, context.Model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Playing, context.Model.State);
            Assert.AreEqual(1, context.AnimationView.IdleCount);
            Assert.AreEqual(0, context.PlayerView.LastLaneIndex);
            Assert.GreaterOrEqual(context.HudView.PlayingCount, 1);
        }

        [Test]
        public void GameHudPresenter_BackToMenu_AsksNavigationPresenter()
        {
            PresenterContext context = new PresenterContext(3);

            context.HudView.RaiseBackToMenu();

            Assert.AreEqual(1, context.SceneNavigationView.LoadMainMenuCount);
        }

        [Test]
        public void MainMenuPresenter_StartAndExit_DelegateToNavigationAndQuitPresenters()
        {
            FakeMainMenuView menuView = new FakeMainMenuView();
            FakeSceneNavigationView sceneView = new FakeSceneNavigationView();
            FakeApplicationQuitView quitView = new FakeApplicationQuitView();
            MainMenuPresenter presenter = new MainMenuPresenter(
                menuView,
                new SceneNavigationPresenter(sceneView),
                new ApplicationQuitPresenter(quitView));

            menuView.RaiseStartGame();
            menuView.RaiseExit();

            Assert.AreEqual(1, sceneView.LoadGameCount);
            Assert.AreEqual(1, quitView.QuitCount);
            presenter.Dispose();
        }

        [Test]
        public void NullViews_AcceptCallsWithoutThrowing()
        {
            Assert.DoesNotThrow(() => NullApplicationQuitView.Instance.Quit());
            Assert.DoesNotThrow(() => NullPlayerAnimationView.Instance.PlayIdle());
            Assert.DoesNotThrow(() => NullPlayerAnimationView.Instance.PlayMoveForward());
            Assert.DoesNotThrow(() => NullPlayerAnimationView.Instance.PlayMoveBackward());
            Assert.DoesNotThrow(() => NullSceneNavigationView.Instance.LoadGame());
            Assert.DoesNotThrow(() => NullSceneNavigationView.Instance.LoadMainMenu());
        }

        private sealed class PresenterContext
        {
            public PresenterContext(int totalLanes)
            {
                Model = new FroggerGameModel(totalLanes);
                InputView = new FakeGameInputView();
                PlayerView = new FakePlayerView();
                AnimationView = new FakePlayerAnimationView();
                HudView = new FakeGameHudView();
                SceneNavigationView = new FakeSceneNavigationView();

                PlayerAnimationPresenter animationPresenter = new PlayerAnimationPresenter(AnimationView);
                PlayerPresenter playerPresenter = new PlayerPresenter(Model, PlayerView, animationPresenter);
                GameHudPresenter hudPresenter = new GameHudPresenter(
                    Model,
                    HudView,
                    playerPresenter,
                    animationPresenter,
                    new SceneNavigationPresenter(SceneNavigationView));
                InputPresenter = new GameInputPresenter(
                    Model,
                    InputView,
                    playerPresenter,
                    animationPresenter,
                    hudPresenter);
            }

            public FroggerGameModel Model { get; }
            public FakeGameInputView InputView { get; }
            public FakePlayerView PlayerView { get; }
            public FakePlayerAnimationView AnimationView { get; }
            public FakeGameHudView HudView { get; }
            public FakeSceneNavigationView SceneNavigationView { get; }
            public GameInputPresenter InputPresenter { get; }
        }

        private sealed class FakeGameInputView : IGameInputView
        {
            public event Action MoveForwardRequested;
            public event Action MoveBackwardRequested;
            public event Action RestartRequested;

            public void RaiseMoveForward() => MoveForwardRequested?.Invoke();
            public void RaiseMoveBackward() => MoveBackwardRequested?.Invoke();
            public void RaiseRestart() => RestartRequested?.Invoke();
        }

        private sealed class FakePlayerView : IPlayerView
        {
            public event Action ObstacleTouched;

            public int LastLaneIndex { get; private set; }
            public bool LastAnimated { get; private set; }

            public void ShowLane(int laneIndex, bool animated)
            {
                LastLaneIndex = laneIndex;
                LastAnimated = animated;
            }

            public void RaiseObstacleTouched() => ObstacleTouched?.Invoke();
        }

        private sealed class FakeGameHudView : IGameHudView
        {
            public event Action PlayAgainRequested;
            public event Action BackToMenuRequested;

            public int PlayingCount { get; private set; }
            public int WonCount { get; private set; }
            public int LastCurrentLaneIndex { get; private set; }

            public void ShowPlaying(int currentLaneIndex, int goalLaneIndex)
            {
                PlayingCount++;
                LastCurrentLaneIndex = currentLaneIndex;
            }

            public void ShowWon()
            {
                WonCount++;
            }

            public void RaisePlayAgain() => PlayAgainRequested?.Invoke();
            public void RaiseBackToMenu() => BackToMenuRequested?.Invoke();
        }

        private sealed class FakePlayerAnimationView : IPlayerAnimationView
        {
            public int IdleCount { get; private set; }
            public int ForwardCount { get; private set; }
            public int BackwardCount { get; private set; }

            public void PlayIdle() => IdleCount++;
            public void PlayMoveForward() => ForwardCount++;
            public void PlayMoveBackward() => BackwardCount++;
        }

        private sealed class FakeSceneNavigationView : ISceneNavigationView
        {
            public int LoadGameCount { get; private set; }
            public int LoadMainMenuCount { get; private set; }

            public void LoadGame() => LoadGameCount++;
            public void LoadMainMenu() => LoadMainMenuCount++;
        }

        private sealed class FakeApplicationQuitView : IApplicationQuitView
        {
            public int QuitCount { get; private set; }

            public void Quit() => QuitCount++;
        }

        private sealed class FakeMainMenuView : IMainMenuView
        {
            public event Action StartGameRequested;
            public event Action ExitRequested;

            public void RaiseStartGame() => StartGameRequested?.Invoke();
            public void RaiseExit() => ExitRequested?.Invoke();
        }

        private sealed class FakeCarSpawnerView : ICarSpawnerView
        {
            public int SpawnCount { get; private set; }

            public void SpawnCar() => SpawnCount++;
        }

        private sealed class FakeObstacleView : IObstacleView
        {
            public float TotalMoved { get; private set; }

            public void Move(float distance)
            {
                TotalMoved += distance;
            }
        }
    }
}
