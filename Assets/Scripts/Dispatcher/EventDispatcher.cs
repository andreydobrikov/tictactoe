using System;
using System.Collections.Generic;

namespace com.tictactoe.dispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private delegate void EventListener(IEvent e);

        private Dictionary<Type, EventListener> listeners = new Dictionary<Type, EventListener>();
        private Dictionary<Delegate, EventListener> listenerLookup = new Dictionary<Delegate, EventListener>();

        public void AddListener<T>(EventListener<T> eventListener) where T : IEvent
        {
            if (listenerLookup.ContainsKey(eventListener))
            {
                return;
            }

            // Create a new non-generic Listener which calls our generic one.  This
            // is the Listener we actually invoke.
            EventListener internalListener = (e) => eventListener((T)e);
            listenerLookup[eventListener] = internalListener;

            EventListener tempDel;
            if (listeners.TryGetValue(typeof(T), out tempDel))
            {
                listeners[typeof(T)] = tempDel += internalListener;
            }
            else
            {
                listeners[typeof(T)] = internalListener;
            }
        }

        public void RemoveListener<T>(EventListener<T> eventListener) where T : IEvent
        {
            EventListener internalListener;
            if (listenerLookup.TryGetValue(eventListener, out internalListener))
            {
                EventListener tempDel;
                if (listeners.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalListener;
                    if (tempDel == null)
                    {
                        listeners.Remove(typeof(T));
                    }
                    else
                    {
                        listeners[typeof(T)] = tempDel;
                    }
                }

                listenerLookup.Remove(eventListener);
            }
        }

        public int ListenerLookupCount { get { return listenerLookup.Count; } }

        public void Dispatch(IEvent e)
        {
            EventListener eventListener;
            if (listeners.TryGetValue(e.GetType(), out eventListener))
            {
                eventListener.Invoke(e);
            }
        }

        public void RemoveAllListeners()
        {
            listeners = new Dictionary<Type, EventListener>();
            listenerLookup = new Dictionary<Delegate, EventListener>();
        }
    }
}
