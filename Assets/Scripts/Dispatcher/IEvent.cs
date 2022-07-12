namespace com.tictactoe.dispatcher
{
    public interface IEvent{}
    public class Event : IEvent {}

  //  public interface IEvent<TEventModel> where TEventModel : IEventModel { }
    public interface IEventModel {}
    public class Event<TEventModel> : IEvent where TEventModel : IEventModel
    {
        public TEventModel model { get; set; }
    }

    public class EventFactory : Singleton<EventFactory>
    {
        public TEvent Create<TEvent, TEventModel>(TEventModel model)
            where TEventModel : IEventModel
            where TEvent : Event<TEventModel>, new()
        {
            TEvent evt = new TEvent();
            evt.model = model;
            return evt;
        }
    }

    public delegate void EventListener<T>(T e) where T : IEvent;
    public delegate void EventListener();
}
