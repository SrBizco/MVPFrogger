using System;
using MVPFrogger.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace MVPFrogger.Views
{
    public sealed class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button exitButton;

        public event Action StartGameRequested;
        public event Action ExitRequested;

        private void OnEnable()
        {
            AddListener(startGameButton, OnStartGameClicked);
            AddListener(exitButton, OnExitClicked);
        }

        private void OnDisable()
        {
            RemoveListener(startGameButton, OnStartGameClicked);
            RemoveListener(exitButton, OnExitClicked);
        }

        private void OnStartGameClicked()
        {
            StartGameRequested?.Invoke();
        }

        private void OnExitClicked()
        {
            ExitRequested?.Invoke();
        }

        private static void AddListener(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }

        private static void RemoveListener(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(action);
            }
        }
    }
}
