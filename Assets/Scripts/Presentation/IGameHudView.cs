namespace MVPFrogger.Presentation
{
    public interface IGameHudView
    {
        void ShowPlaying(int currentLaneIndex, int goalLaneIndex);
        void ShowWon();
    }
}
