using System;
using MVPFrogger.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace MVPFrogger.Views
{
    public sealed class GameHudView : MonoBehaviour, IGameHudView
    {
        [Header("Screens")]
        [SerializeField] private GameObject playingRoot;
        [SerializeField] private GameObject victoryRoot;

        [Header("Buttons")]
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button backToMenuButton;

        public event Action PlayAgainRequested;
        public event Action BackToMenuRequested;

        private void OnEnable()
        {
            AddListener(playAgainButton, OnPlayAgainClicked);
            AddListener(backToMenuButton, OnBackToMenuClicked);
        }

        private void OnDisable()
        {
            RemoveListener(playAgainButton, OnPlayAgainClicked);
            RemoveListener(backToMenuButton, OnBackToMenuClicked);
        }

        public void ShowPlaying(int currentLaneIndex, int goalLaneIndex)
        {
            SetActive(playingRoot, true);
            SetActive(victoryRoot, false);
        }

        public void ShowWon()
        {
            SetActive(playingRoot, false);
            SetActive(victoryRoot, true);
        }

        private void OnPlayAgainClicked()
        {
            PlayAgainRequested?.Invoke();
        }

        private void OnBackToMenuClicked()
        {
            BackToMenuRequested?.Invoke();
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

        private static void SetActive(GameObject target, bool isActive)
        {
            if (target != null)
            {
                target.SetActive(isActive);
            }
        }
    }
}
