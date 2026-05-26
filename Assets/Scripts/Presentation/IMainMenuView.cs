using System;

namespace MVPFrogger.Presentation
{
    public interface IMainMenuView
    {
        event Action StartGameRequested;
        event Action ExitRequested;
    }
}
