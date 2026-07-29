using UnityEngine;
using UnityEngine.SceneManagement;

public class staticObjectBase : MonoBehaviour { };

public class staticObject<T> : staticObjectBase where T : MonoBehaviour
{
    public static T Instance_;

    protected virtual void Awake()
    {
        if (Instance_ != null && Instance_ != this as T) {
            Destroy(gameObject);
            return;
        }
        Instance_ = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnEnable()
    {
        staticSceneManager.Instance_.AddOnLoaded(OnLoadedScene);
    }
    protected virtual void OnDisable()
    {
        staticSceneManager.Instance_.LeftOnLoaded(OnLoadedScene);
    }

    protected virtual void OnLoadedScene(Scene scene, LoadSceneMode mode){}
}