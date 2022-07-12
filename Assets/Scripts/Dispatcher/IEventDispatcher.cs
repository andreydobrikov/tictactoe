namespace com.tictactoe.dispatcher
{
    public interface IEventDispatcher
    {
        void AddListener<T>(EventListener<T> listener) where T : IEvent;
        void RemoveListener<T>(EventListener<T> listener) where T : IEvent;
        void Dispatch(IEvent e);
        void RemoveAllListeners();
    }
}
