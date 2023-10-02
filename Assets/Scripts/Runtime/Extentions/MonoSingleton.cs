using UnityEngine;

namespace Runtime.Extentions
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                _instance = FindObjectOfType<T>();
                /*
                    var instances = FindObjectsOfType<T>();

                    for (int i = 0; i < instances.Length - 1; i++)
                    {
                        Destroy(instances[i]);
                    }
                
                    _instance = instances[0];
                */

                    if (_instance == null)
                    {
                        GameObject newGameObject = new GameObject(typeof(T).Name);
                        _instance = newGameObject.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
        protected void Awake()
        {
            _instance = this as T;
        }
    }
}


