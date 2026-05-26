using MVPFrogger.Presentation;
using UnityEngine;

namespace MVPFrogger.Views
{
    public sealed class UnityApplicationQuitView : MonoBehaviour, IApplicationQuitView
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}
