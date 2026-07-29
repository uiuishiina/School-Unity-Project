using UnityEngine;

public class GameManager : MonoBehaviour
{
    staticInput staticInput_;

    private void Start()
    {
        staticInput_ = staticInput.Instance_;
        staticInput_.ChengeActionMap("Player");
    }
}
