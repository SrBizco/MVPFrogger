namespace MVPFrogger.Presentation
{
    public sealed class NullApplicationQuitView : IApplicationQuitView
    {
        public static readonly NullApplicationQuitView Instance = new NullApplicationQuitView();

        private NullApplicationQuitView()
        {
        }

        public void Quit()
        {
        }
    }
}
