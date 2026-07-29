using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    staticCameraManager staticCamera_;

    [Header("UI")]
    [SerializeField] private Canvas canvas_;

    private void Start()
    {
        staticCamera_ = staticCameraManager.Instance_;

        canvas_.worldCamera = staticCamera_.GetCamera();
    }
}
