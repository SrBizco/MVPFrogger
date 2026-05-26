using System;

namespace MVPFrogger.Presentation
{
    public interface IPlayerView
    {
        event Action ObstacleTouched;
        void ShowLane(int laneIndex, bool animated);
    }
}
