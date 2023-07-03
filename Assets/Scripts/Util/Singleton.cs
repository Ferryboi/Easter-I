using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                bool instanceSet = SetInstance(FindObjectOfType<T>());

                if(!instanceSet)
                {
                    //GameObject iObject = new GameObject($"{typeof(T)} (Singleton)");
                    //SetInstance(iObject.AddComponent<T>());
                }
            }

            return instance;
        }

        protected set
        {
            if (instance == null)
            {
                SetInstance(value);
                instance.gameObject.name = typeof(T) + " [Singleton]";
            }
        }
    }

    protected static bool SetInstance(T setInstance)
    {
        if (setInstance == null || instance != null) return false;

        instance = setInstance;
        return true;
    }

    protected static void DestroyInstance()
    {
        instance = null;
    }
}
