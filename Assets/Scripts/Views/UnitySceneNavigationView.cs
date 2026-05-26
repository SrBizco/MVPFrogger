using MVPFrogger.Presentation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVPFrogger.Views
{
    public sealed class UnitySceneNavigationView : MonoBehaviour, ISceneNavigationView
    {
        [SerializeField] private string gameSceneName = "GameScene";
        [SerializeField] private string mainMenuSceneName = "MainMenuScene";

        public void LoadGame()
        {
            LoadScene(gameSceneName);
        }

        public void LoadMainMenu()
        {
            LoadScene(mainMenuSceneName);
        }

        private static void LoadScene(string sceneName)
        {
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
