using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class GameHudView : MonoBehaviour, IGameHudView
    {
        [SerializeField] private GameObject playingRoot;
        [SerializeField] private GameObject wonRoot;

        public void ShowPlaying(int currentLaneIndex, int goalLaneIndex)
        {
            SetActive(playingRoot, true);
            SetActive(wonRoot, false);
        }

        public void ShowWon()
        {
            SetActive(playingRoot, false);
            SetActive(wonRoot, true);
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
