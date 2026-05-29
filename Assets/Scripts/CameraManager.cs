using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField, Header("移動オフセット")] private Vector2 CameraOffset = new Vector2(3,5);
    [SerializeField, Header("原点オフセット")] private Vector3 originoffset = new Vector3(-2, 0, 0);
    [SerializeField, Header("追従ターゲット")] private GameObject Target;

    private void Start()
    {
        MainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (Target == null) return;

        Vector3 diff = Target.transform.position - transform.position;

        Vector3 move = new Vector3(
            ApplyDeadZone(diff.x, CameraOffset.x),
            ApplyDeadZone(diff.y, CameraOffset.y),
            0
        );

        // 必要な分だけ移動
        transform.position += move;
    }

    /// <summary>
    /// 画面移動オフセット計算処理関数
    /// </summary>
    float ApplyDeadZone(float value, float offset)
    {
        //符号を外して計算しやすくする
        float abs = Mathf.Abs(value);

        //オフセット以下なら0
        if (abs <= offset) {
            return 0f;
        }
        
        //オフセット以上なら必要な移動量を返す
        return (abs - offset) * Mathf.Sign(value);
    }
}
