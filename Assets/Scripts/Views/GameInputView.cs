using System;
using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class GameInputView : MonoBehaviour, IGameInputView
    {
        [SerializeField] private KeyCode moveForwardKey = KeyCode.W;
        [SerializeField] private KeyCode moveBackwardKey = KeyCode.S;
        [SerializeField] private KeyCode restartKey = KeyCode.R;

        public event Action MoveForwardRequested;
        public event Action MoveBackwardRequested;
        public event Action RestartRequested;

        private void Update()
        {
            if (Input.GetKeyDown(moveForwardKey))
            {
                MoveForwardRequested?.Invoke();
            }

            if (Input.GetKeyDown(moveBackwardKey))
            {
                MoveBackwardRequested?.Invoke();
            }

            if (Input.GetKeyDown(restartKey))
            {
                RestartRequested?.Invoke();
            }
        }
    }
}
