using System.Collections;
using System.Reflection;
using MVPFrogger.Configuration;
using MVPFrogger.Views;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace MVPFrogger.Tests.PlayMode
{
    public sealed class ViewPlayModeTests
    {
        [UnityTest]
        public IEnumerator PlayerView_ShowLaneInstantly_MovesTargetToLanePoint()
        {
            GameObject player = new GameObject("Player");
            PlayerView view = player.AddComponent<PlayerView>();
            Transform lane = new GameObject("Lane").transform;
            lane.position = new Vector3(3f, 0f, 5f);
            SetPrivateField(view, "lanePoints", new[] { lane });

            view.ShowLane(0, false);

            Assert.AreEqual(lane.position, player.transform.position);
            Object.Destroy(player);
            Object.Destroy(lane.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerView_ShowLaneAnimated_ReachesLanePointAfterDuration()
        {
            GameObject player = new GameObject("Player");
            PlayerView view = player.AddComponent<PlayerView>();
            Transform lane = new GameObject("Lane").transform;
            lane.position = new Vector3(0f, 0f, 4f);
            SetPrivateField(view, "lanePoints", new[] { lane });
            SetPrivateField(view, "movementDuration", 0.05f);

            view.ShowLane(0, true);
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(lane.position, player.transform.position);
            Object.Destroy(player);
            Object.Destroy(lane.gameObject);
        }

        [UnityTest]
        public IEnumerator PlayerView_OnTriggerEnterWithObstacleLayer_RaisesObstacleTouched()
        {
            GameObject player = new GameObject("Player");
            PlayerView view = player.AddComponent<PlayerView>();
            SetPrivateField(view, "obstacleLayers", new LayerMask { value = 1 << 7 });
            GameObject obstacle = new GameObject("Obstacle");
            obstacle.layer = 7;
            BoxCollider obstacleCollider = obstacle.AddComponent<BoxCollider>();
            bool wasRaised = false;
            view.ObstacleTouched += () => wasRaised = true;

            InvokePrivateMethod(view, "OnTriggerEnter", obstacleCollider);

            Assert.IsTrue(wasRaised);
            Object.Destroy(player);
            Object.Destroy(obstacle);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ObstacleView_Move_UsesTargetForward()
        {
            GameObject obstacle = new GameObject("Obstacle");
            ObstacleView view = obstacle.AddComponent<ObstacleView>();
            obstacle.transform.rotation = Quaternion.LookRotation(Vector3.right);

            view.Move(2f);

            Assert.That(Vector3.Distance(new Vector3(2f, 0f, 0f), obstacle.transform.position), Is.LessThan(0.001f));
            Object.Destroy(obstacle);
            yield return null;
        }

        [UnityTest]
        public IEnumerator GameHudView_ShowWon_ActivatesVictoryAndHidesPlaying()
        {
            GameObject hud = new GameObject("Hud");
            GameObject playingRoot = new GameObject("PlayingRoot");
            GameObject victoryRoot = new GameObject("VictoryRoot");
            GameHudView view = hud.AddComponent<GameHudView>();
            SetPrivateField(view, "playingRoot", playingRoot);
            SetPrivateField(view, "victoryRoot", victoryRoot);

            view.ShowWon();

            Assert.IsFalse(playingRoot.activeSelf);
            Assert.IsTrue(victoryRoot.activeSelf);
            Object.Destroy(hud);
            Object.Destroy(playingRoot);
            Object.Destroy(victoryRoot);
            yield return null;
        }

        [UnityTest]
        public IEnumerator GameHudView_ShowPlaying_ActivatesPlayingAndHidesVictory()
        {
            GameObject hud = new GameObject("Hud");
            GameObject playingRoot = new GameObject("PlayingRoot");
            GameObject victoryRoot = new GameObject("VictoryRoot");
            GameHudView view = hud.AddComponent<GameHudView>();
            SetPrivateField(view, "playingRoot", playingRoot);
            SetPrivateField(view, "victoryRoot", victoryRoot);

            view.ShowPlaying(1, 3);

            Assert.IsTrue(playingRoot.activeSelf);
            Assert.IsFalse(victoryRoot.activeSelf);
            Object.Destroy(hud);
            Object.Destroy(playingRoot);
            Object.Destroy(victoryRoot);
            yield return null;
        }

        [UnityTest]
        public IEnumerator GameHudView_ButtonClicks_RaiseRequestedEvents()
        {
            GameObject hud = new GameObject("Hud");
            hud.SetActive(false);
            GameHudView view = hud.AddComponent<GameHudView>();
            Button playAgainButton = new GameObject("PlayAgain").AddComponent<Button>();
            Button backToMenuButton = new GameObject("BackToMenu").AddComponent<Button>();
            SetPrivateField(view, "playAgainButton", playAgainButton);
            SetPrivateField(view, "backToMenuButton", backToMenuButton);
            int playAgainCount = 0;
            int backToMenuCount = 0;
            view.PlayAgainRequested += () => playAgainCount++;
            view.BackToMenuRequested += () => backToMenuCount++;

            hud.SetActive(true);
            playAgainButton.onClick.Invoke();
            backToMenuButton.onClick.Invoke();

            Assert.AreEqual(1, playAgainCount);
            Assert.AreEqual(1, backToMenuCount);
            Object.Destroy(hud);
            Object.Destroy(playAgainButton.gameObject);
            Object.Destroy(backToMenuButton.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator MainMenuView_ButtonClicks_RaiseRequestedEvents()
        {
            GameObject menu = new GameObject("Menu");
            menu.SetActive(false);
            MainMenuView view = menu.AddComponent<MainMenuView>();
            Button startButton = new GameObject("Start").AddComponent<Button>();
            Button exitButton = new GameObject("Exit").AddComponent<Button>();
            SetPrivateField(view, "startGameButton", startButton);
            SetPrivateField(view, "exitButton", exitButton);
            int startCount = 0;
            int exitCount = 0;
            view.StartGameRequested += () => startCount++;
            view.ExitRequested += () => exitCount++;

            menu.SetActive(true);
            startButton.onClick.Invoke();
            exitButton.onClick.Invoke();

            Assert.AreEqual(1, startCount);
            Assert.AreEqual(1, exitCount);
            Object.Destroy(menu);
            Object.Destroy(startButton.gameObject);
            Object.Destroy(exitButton.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerCameraView_LateUpdate_FollowsAndLooksAtPlayer()
        {
            GameObject cameraObject = new GameObject("Camera");
            PlayerCameraView view = cameraObject.AddComponent<PlayerCameraView>();
            Transform player = new GameObject("Player").transform;
            player.position = new Vector3(0f, 0f, 10f);
            SetPrivateField(view, "player", player);
            SetPrivateField(view, "playerForwardDirection", Vector3.forward);
            SetPrivateField(view, "height", 10f);
            SetPrivateField(view, "distanceBehindPlayer", 6f);
            SetPrivateField(view, "lookAheadDistance", 4f);
            SetPrivateField(view, "followSpeed", 1000f);

            InvokePrivateMethod(view, "LateUpdate");

            Assert.That(Vector3.Distance(new Vector3(0f, 10f, 4f), cameraObject.transform.position), Is.LessThan(0.1f));
            Assert.That(Vector3.Dot(cameraObject.transform.forward, (new Vector3(0f, 0f, 14f) - cameraObject.transform.position).normalized), Is.GreaterThan(0.99f));
            Object.Destroy(cameraObject);
            Object.Destroy(player.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerAnimatorView_WithoutAnimator_DoesNotThrow()
        {
            GameObject player = new GameObject("Player");
            PlayerAnimatorView view = player.AddComponent<PlayerAnimatorView>();

            Assert.DoesNotThrow(() => view.PlayIdle());
            Assert.DoesNotThrow(() => view.PlayMoveForward());
            Assert.DoesNotThrow(() => view.PlayMoveBackward());

            Object.Destroy(player);
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnityCarSpawnerView_SpawnCar_InstantiatesPrefabAtSpawnPoint()
        {
            GameObject spawnerObject = new GameObject("Spawner");
            spawnerObject.SetActive(false);
            UnityCarSpawnerView view = spawnerObject.AddComponent<UnityCarSpawnerView>();
            GameObject carPrefab = new GameObject("CarPrefab");
            Transform spawnPoint = new GameObject("SpawnPoint").transform;
            Transform carsParent = new GameObject("CarsParent").transform;
            spawnPoint.position = new Vector3(4f, 0f, 2f);
            SetPrivateField(view, "carPrefabs", new[] { carPrefab });
            SetPrivateField(view, "spawnPoint", spawnPoint);
            SetPrivateField(view, "carsParent", carsParent);
            SetPrivateField(view, "carMovement", new CarMovementConfig(3f, 10f, true));

            spawnerObject.SetActive(true);
            view.SpawnCar();

            Assert.AreEqual(1, carsParent.childCount);
            Assert.That(Vector3.Distance(spawnPoint.position, carsParent.GetChild(0).position), Is.LessThan(0.001f));
            Object.Destroy(spawnerObject);
            Object.Destroy(carPrefab);
            Object.Destroy(spawnPoint.gameObject);
            Object.Destroy(carsParent.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnitySceneNavigationView_EmptySceneNames_DoNotThrow()
        {
            GameObject navigator = new GameObject("Navigator");
            UnitySceneNavigationView view = navigator.AddComponent<UnitySceneNavigationView>();
            SetPrivateField(view, "gameSceneName", string.Empty);
            SetPrivateField(view, "mainMenuSceneName", string.Empty);

            Assert.DoesNotThrow(() => view.LoadGame());
            Assert.DoesNotThrow(() => view.LoadMainMenu());

            Object.Destroy(navigator);
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnityApplicationQuitView_Quit_DoesNotThrow()
        {
            GameObject application = new GameObject("Application");
            UnityApplicationQuitView view = application.AddComponent<UnityApplicationQuitView>();

            Assert.DoesNotThrow(() => view.Quit());

            Object.Destroy(application);
            yield return null;
        }

        private static void SetPrivateField(object target, string fieldName, object value)
        {
            FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"Field {fieldName} was not found.");
            field.SetValue(target, value);
        }

        private static void InvokePrivateMethod(object target, string methodName, params object[] arguments)
        {
            MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(method, $"Method {methodName} was not found.");
            method.Invoke(target, arguments);
        }
    }
}
