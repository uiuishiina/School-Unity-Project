using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class staticPlayer : staticObject<staticPlayer>
{
    private bool is_enable = false;

    // --- Action --- //
    staticInput staticInput_;
    InputAction spaceAction_;


    protected override void Awake()
    {
        base.Awake();
        if (Instance_ != this) {
            return;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        staticInput_ = staticInput.Instance_;

        spaceAction_ = staticInput_.GetActionMap("Player").FindAction("Space");
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        ActivePlayer(false);
    }

    void ActivePlayer(bool active)
    {
        if (active == is_enable) {
            return;
        }

        if (active) {
            spaceAction_.performed += OnSpace;
        }
        else {
            spaceAction_.performed -= OnSpace;
        }

        is_enable = active;
        Debug.Log($"player = {active}");
    }

    protected override void OnLoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScene") {
            ActivePlayer(false);
        }
        else {
            ActivePlayer(true);
        }
    }

    void OnSpace(InputAction.CallbackContext context)
    {
        Debug.Log("OnSpace");
    }
}
