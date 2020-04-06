namespace Core
{
    public interface IController<TView> where TView : IView
    {
        void OnOpen(TView view);
        void OnClose(TView view);
    }
}