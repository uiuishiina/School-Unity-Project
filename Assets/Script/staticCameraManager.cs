using UnityEngine;
using UnityEngine.SceneManagement;

public class staticCameraManager : staticObject<staticCameraManager>
{
    private Camera camera_;

    protected override void Awake()
    {
        base.Awake();
        if (Instance_ != this) {
            return;
        }
    }

    protected override void OnLoadedScene(Scene scene, LoadSceneMode mode)
    {
        camera_ = FindFirstObjectByType<Camera>();
    }

    public Camera GetCamera()
    {
        if (camera_ == null) {
            Debug.Log("NO Camera");
        }
        return camera_;
    }
}
