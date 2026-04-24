using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Pool;
using Vector3 = UnityEngine.Vector3;

public class FloorPoolManager : MonoBehaviour
{
    [SerializeField, Header("プレイヤー")] private GameObject Player_;
    [SerializeField,Header("床Prefab")] private GameObject FloorPrefab_;
    private ObjectPool<GameObject> FloorPool_;
    [SerializeField] private int LeftposX_ = 0;
    [SerializeField] private int RightposX_ = 0;
    [SerializeField, Header("床の長さ")] private int FloorLength_ = 5;

    List<GameObject> floorvec_ = new List<GameObject>();

    void Start()
    {
        floorvec_.Clear();
        //長さ初期化
        FloorLength_ = FloorLength_ < 3 ? 3 : FloorLength_; //3以上固定
        FloorLength_ = FloorLength_ % 2 == 0 ? FloorLength_ + 1 : FloorLength_; //奇数にする
        //オブジェクトプール作成
        FloorPool_ = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(FloorPrefab_, transform), // 1. 生成
            actionOnGet: (obj) => obj.SetActive(true),              // 2. 取得時（表示）
            actionOnRelease: (obj) => obj.SetActive(false),         // 3. 返却時（非表示）
            actionOnDestroy: (obj) => Destroy(obj),                 // 4. 破棄時
            collectionCheck: true, // 重複返却時のエラーチェック
            defaultCapacity: FloorLength_,    // 初期容量
            maxSize: FloorLength_ + 1           // 最大容量
        );

        //右側を出す
        RightposX_ = (FloorLength_ - 1) / 2 * 5;
        LeftposX_ = (FloorLength_ - 1) / 2 * -5;
        //床作成
        for (int i = 0; i < FloorLength_; i++)
        {
            var floor = FloorPool_.Get();
            var posx = RightposX_ - (i * 5);
            floor.transform.position = new Vector3(posx, -1, 0);
            floorvec_.Add(floor);
        }
    }

    void Update()
    {
        if(Player_.transform.position.x < LeftposX_)
        {
            FloorRelease(-1);
        }
        else if(Player_.transform.position.x > RightposX_)
        {
            FloorRelease(1);
        }
    }

    void FloorRelease(int sign)
    {
        var maxdest = 0.0f;
        GameObject obj = null;
        foreach (var floor in floorvec_) {
            var dest = Vector3.Distance(Player_.transform.position, floor.transform.position);
            if (maxdest < dest)
            {
                maxdest = dest;
                obj = floor;
            }
        }
        if (obj == null) { Debug.LogError("floor = null"); return; }
        FloorPool_.Release(obj);

        var newfloor = FloorPool_.Get();
        var posx = sign < 0 ? (LeftposX_ - 5) : (RightposX_ + 5);
        newfloor.transform.position=new Vector3(posx, -1, 0);

        if (sign < 0)
        {
            LeftposX_ = posx;
            RightposX_ -= 5;
        }
        else
        {
            RightposX_= posx;
            LeftposX_ += 5;
        }
    }
}
