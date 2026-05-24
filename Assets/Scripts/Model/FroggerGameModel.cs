using System;

namespace MVPFrogger.Model
{
    public sealed class FroggerGameModel
    {
        private readonly int totalLanes;

        public FroggerGameModel(int totalLanes)
        {
            if (totalLanes < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(totalLanes), "Frogger needs at least a start lane and a goal lane.");
            }

            this.totalLanes = totalLanes;
            CurrentLaneIndex = 0;
            State = GameState.Playing;
        }

        public int CurrentLaneIndex { get; private set; }
        public int TotalLanes => totalLanes;
        public int GoalLaneIndex => totalLanes - 1;
        public GameState State { get; private set; }

        public void MoveForward()
        {
            if (State != GameState.Playing)
            {
                return;
            }

            if (CurrentLaneIndex < GoalLaneIndex)
            {
                CurrentLaneIndex++;
            }

            if (CurrentLaneIndex == GoalLaneIndex)
            {
                State = GameState.Won;
            }
        }

        public void MoveBackward()
        {
            if (State != GameState.Playing || CurrentLaneIndex == 0)
            {
                return;
            }

            CurrentLaneIndex--;
        }

        public void HitObstacle()
        {
            if (State != GameState.Playing)
            {
                return;
            }

            CurrentLaneIndex = 0;
        }

        public void Restart()
        {
            CurrentLaneIndex = 0;
            State = GameState.Playing;
        }
    }
}
