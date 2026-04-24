using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("プレイヤー")] private GameObject Player_;
    [SerializeField, Header("メインカメラ")] private Camera MainCamera_;
    [SerializeField, Header("X軸のオフセット上限")] float2 offsetX_ = new float2();
    [SerializeField, Header("Y軸のオフセット上限")] float2 offsetY_ = new float2();
    void Start()
    {
        
    }

    void Update()
    {
        var Ppos = Player_.transform.position;
        var vec2 = new Vector2();
        vec2 += IsVailed(Ppos.x,offsetX_);
        vec2 += IsVailed(Ppos.y,offsetY_);
        Cposchenge(vec2);
    }

    Vector2 IsVailed(float Ppos,float2 offset)
    {
        var vec = new Vector2();
        if (offset.x < Ppos) {
            vec.x = Ppos - offset.x;
        }
        else if (offset.y > Ppos) {
            vec.y = Ppos - offset.y;
        }
        return vec;
    }

    void Cposchenge(Vector2 pos)
    {
        var cpos = MainCamera_.transform.position;
        cpos.x= pos.x; cpos.y = pos.y;
        MainCamera_.transform.position = cpos;
    }
}
