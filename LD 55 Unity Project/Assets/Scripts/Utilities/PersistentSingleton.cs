using UnityEngine;

  public abstract class PersistentSingleton<T> : MonoBehaviour where T : Object
  {
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
      if (Instance != null)
      {
        Debug.Log($"There's already a singleton for type {typeof(T).Name}, so I'll off myself...");
        Destroy(this.gameObject);
        return;
      }

      Instance = this as T;
      DontDestroyOnLoad(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
      if (Instance == this as T)
      {
        Debug.Log($"I'm getting destroyed, so I'll free up the {typeof(T).Name} instance");
        Instance = null;
      }
    }
  }

  public abstract class Singleton<T> : MonoBehaviour where T : Object
  {
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
      if (Instance != null)
      {
        Debug.Log($"There's already a singleton for type {typeof(T).Name}, so I'll off myself...");
        Destroy(this.gameObject);
        return;
      }

      Instance = this as T;
    }

    protected virtual void OnDestroy()
    {
      if (Instance == this as T)
      {
        Debug.Log($"I'm getting destroyed, so I'll free up the {typeof(T).Name} instance");
        Instance = null;
      }
    }
  }