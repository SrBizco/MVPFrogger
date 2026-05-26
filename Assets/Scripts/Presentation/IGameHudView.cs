using System;

namespace MVPFrogger.Presentation
{
    public interface IGameHudView
    {
        event Action PlayAgainRequested;
        event Action BackToMenuRequested;

        void ShowPlaying(int currentLaneIndex, int goalLaneIndex);
        void ShowWon();
    }
}
