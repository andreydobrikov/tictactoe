using System.Collections;

namespace com.tictactoe.dispatcher
{
    public class GlobalEventDispatcher : Singleton<GlobalEventDispatcher>
    {
        public IEventDispatcher dispatcher;

        protected override void Awake()
        {
            dispatcher = new EventDispatcher();
			base.Awake();
        }

        protected virtual void OnDestroy()
        {
            dispatcher.RemoveAllListeners();
            dispatcher = null;
        }
    }
}

