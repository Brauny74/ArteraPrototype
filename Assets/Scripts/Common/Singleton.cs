using UnityEngine;

namespace TopDownShooter
{
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance != null)
                    {
                        return _instance;
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (!Application.isPlaying)
                return;

            _instance = this as T;
        }
    }
}