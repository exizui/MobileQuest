using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface ISingleton
{
    void ResetState();
}

public abstract class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        //DontDestroyOnLoad(gameObject);
    }

    public virtual void ResetState() { }

    public static void DestroyInstance()
    {
        if (instance != null)
        {
            Destroy((instance as MonoBehaviour).gameObject);
            instance = null;
        }
    }
}

