using UnityEngine;

public class LVLManagerLoader<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            else if (_instance != FindObjectOfType<T>())
            {
                Destroy(FindObjectOfType<T>());
            }

            DontDestroyOnLoad(FindObjectOfType<T>());

            return _instance;
        }
    }
}
