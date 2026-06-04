using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventGettor : MonoBehaviour
{
    [SerializeField, Header("呼び出すイベント")] private UnityEvent GuardEvent;
    [SerializeField, Header("呼び出すイベント")] private UnityEvent EndEvent;

    public void CallGuardEvent()
    {
        GuardEvent?.Invoke();
    }
    public void CallEndEvent()
    {
        EndEvent?.Invoke();
    }
}
