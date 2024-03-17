using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T instance;

    public void Initialize(T t)
    {
        if (instance == null)
        {
            instance = t;

            DontDestroyOnLoad(gameObject);

            return;
        }

        Destroy(gameObject);
    }
}
