using UnityEngine;

namespace com.tictactoe.dispatcher
{
    public class MonoBehaviourEventDispatcher : MonoBehaviour
    {
        public IEventDispatcher dispatcher;

        protected virtual void Awake()
        {
            dispatcher = new EventDispatcher();
        }

        protected virtual void OnDestroy()
        {
            dispatcher.RemoveAllListeners();
            dispatcher = null;
        }
    }
}
