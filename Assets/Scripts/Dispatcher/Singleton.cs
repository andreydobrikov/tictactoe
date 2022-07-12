using UnityEngine;

namespace com.tictactoe.dispatcher
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    return null;
                }

                lock (m_Lock)
                {
                    if (instance == null)
                    {
                        // Search for existing instance.
                        instance = (T)FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            //create an only instance of it only
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = gameObject.GetComponent<T>();
                if (instance == null)
                {
                    instance = gameObject.AddComponent<T>();
                }
            }
            else
            {
                //if we already had an instance of this object, we need to immidiately destroy ourselves
                Destroy(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }


        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }
    }
}
