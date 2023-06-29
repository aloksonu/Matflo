using UnityEngine;

namespace Utilities
{
    public class PersistentMonoSingleton<T> : MonoSingleton<T> where T : Component
    {
        protected override void Awake()
        {
            base.Awake();
            if (Instance)
                DontDestroyOnLoad(gameObject);
        }
    }
}