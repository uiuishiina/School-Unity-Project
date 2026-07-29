using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class staticSceneManager : MonoBehaviour
{
    public static staticSceneManager Instance_;

    private void Awake()
    {
        if(Instance_ != null && Instance_ != this) {
            Destroy(gameObject);
            return;
        }
        Instance_ = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void AddOnLoaded(UnityAction<Scene,LoadSceneMode> func)
    {
        SceneManager.sceneLoaded += func;
    }
    public void LeftOnLoaded(UnityAction<Scene, LoadSceneMode> func)
    {
        SceneManager.sceneLoaded -= func;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
