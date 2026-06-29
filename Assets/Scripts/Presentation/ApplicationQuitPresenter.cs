namespace MVPFrogger.Presentation
{
    public sealed class ApplicationQuitPresenter
    {
        private readonly IApplicationQuitView view;

        public ApplicationQuitPresenter(IApplicationQuitView view)
        {
            this.view = view;
        }

        public void Quit()
        {
            view.Quit();
        }
    }
}
