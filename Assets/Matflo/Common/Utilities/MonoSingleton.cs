using UnityEngine;

namespace Utilities
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                  //  Debug.LogError(typeof(T) + " is NULL");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
               //  Debug.Log(typeof(T) + " Initialized");
            }
            else
            {
                Destroy(gameObject);
            }

            Init();
        }

        protected virtual void Init()
        {
            //Optional Overridable Method
        }
    }
}