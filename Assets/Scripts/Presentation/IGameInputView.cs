using System;

namespace MVPFrogger.Presentation
{
    public interface IGameInputView
    {
        event Action MoveForwardRequested;
        event Action MoveBackwardRequested;
        event Action RestartRequested;
    }
}
