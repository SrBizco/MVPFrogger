using System;
using MVPFrogger.Model;
using NUnit.Framework;

namespace MVPFrogger.Tests.EditMode
{
    public sealed class ModelTests
    {
        [Test]
        public void FroggerGameModel_InvalidLaneAmount_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FroggerGameModel(1));
        }

        [Test]
        public void FroggerGameModel_NewGame_StartsPlayingAtFirstLane()
        {
            FroggerGameModel model = new FroggerGameModel(4);

            Assert.AreEqual(0, model.CurrentLaneIndex);
            Assert.AreEqual(3, model.GoalLaneIndex);
            Assert.AreEqual(GameState.Playing, model.State);
        }

        [Test]
        public void FroggerGameModel_MoveForward_ReachesGoalAndWins()
        {
            FroggerGameModel model = new FroggerGameModel(3);

            model.MoveForward();
            model.MoveForward();

            Assert.AreEqual(2, model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Won, model.State);
        }

        [Test]
        public void FroggerGameModel_MoveBackward_DoesNotGoBelowFirstLane()
        {
            FroggerGameModel model = new FroggerGameModel(3);

            model.MoveBackward();

            Assert.AreEqual(0, model.CurrentLaneIndex);
        }

        [Test]
        public void FroggerGameModel_HitObstacle_WhenPlaying_ReturnsToFirstLane()
        {
            FroggerGameModel model = new FroggerGameModel(4);
            model.MoveForward();
            model.MoveForward();

            model.HitObstacle();

            Assert.AreEqual(0, model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Playing, model.State);
        }

        [Test]
        public void FroggerGameModel_RestartAfterWin_ReturnsToPlaying()
        {
            FroggerGameModel model = new FroggerGameModel(2);
            model.MoveForward();

            model.Restart();

            Assert.AreEqual(0, model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Playing, model.State);
        }

        [Test]
        public void FroggerGameModel_AfterWin_IgnoresMovementAndObstacleHit()
        {
            FroggerGameModel model = new FroggerGameModel(2);
            model.MoveForward();

            model.MoveForward();
            model.MoveBackward();
            model.HitObstacle();

            Assert.AreEqual(1, model.CurrentLaneIndex);
            Assert.AreEqual(GameState.Won, model.State);
        }

        [Test]
        public void CarSpawnerModel_InvalidInterval_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CarSpawnerModel(0f, 0f));
        }

        [Test]
        public void CarSpawnerModel_AdvanceBeforeInterval_DoesNotSpawn()
        {
            CarSpawnerModel model = new CarSpawnerModel(2f, 0f);

            bool shouldSpawn = model.Advance(1f);

            Assert.IsFalse(shouldSpawn);
        }

        [Test]
        public void CarSpawnerModel_AdvanceAtInterval_SpawnsAndResetsTimer()
        {
            CarSpawnerModel model = new CarSpawnerModel(2f, 0f);

            Assert.IsTrue(model.Advance(2f));
            Assert.IsFalse(model.Advance(1f));
            Assert.IsTrue(model.Advance(1f));
        }

        [Test]
        public void CarSpawnerModel_InitialDelay_PostponesFirstSpawn()
        {
            CarSpawnerModel model = new CarSpawnerModel(2f, 1f);

            Assert.IsFalse(model.Advance(2f));
            Assert.IsTrue(model.Advance(1f));
        }

        [Test]
        public void ObstacleModel_InvalidTravelDistance_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new ObstacleModel(3f, 0f));
        }

        [Test]
        public void ObstacleModel_Advance_UsesAbsoluteSpeed()
        {
            ObstacleModel model = new ObstacleModel(-3f, 10f);

            model.Advance(2f);

            Assert.AreEqual(6f, model.TravelledDistance);
            Assert.IsFalse(model.ReachedRouteEnd);
        }

        [Test]
        public void ObstacleModel_ReachesRouteEnd_WhenTravelledDistanceIsEnough()
        {
            ObstacleModel model = new ObstacleModel(5f, 10f);

            model.Advance(2f);

            Assert.IsTrue(model.ReachedRouteEnd);
        }
    }
}
