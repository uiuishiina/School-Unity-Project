using DG.Tweening;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Text = UnityEngine.UI.Text;

public class TitleManager : MonoBehaviour
{
    staticInput staticInput_;
    InputAction SpaceAction_;

    Sequence spaceSequence_;

    [Header("UI")]
    [SerializeField] private Text naviText_;
    [SerializeField] private string textList_;
    [SerializeField] private string spaceText_;
    
    private void Start()
    {
        staticInput_ = staticInput.Instance_;
        SpaceAction_ = staticInput_.GetActionMap("UI").FindAction("Space");
        SpaceAction_.performed += PushSpace;
        staticInput_.ChengeActionMap("UI");

        naviText_.DOText(textList_, 0.5f).SetDelay(2f);

        spaceSequence_ = DOTween.Sequence();
        spaceSequence_.Pause();
        spaceSequence_.Append(naviText_.DOText(spaceText_, 2.0f,true,ScrambleMode.All));
        spaceSequence_.Append(DOVirtual.DelayedCall(2,() => {
            //wait
        }));
        spaceSequence_.OnComplete(() => {
            FindFirstObjectByType<staticSceneManager>().LoadScene("GameScene");
        });
    }

    private void OnDestroy()
    {
        SpaceAction_.performed -= PushSpace;
        spaceSequence_?.Kill();
    }

    private void PushSpace(InputAction.CallbackContext context)
    {
        spaceSequence_?.Play();
    }
}
