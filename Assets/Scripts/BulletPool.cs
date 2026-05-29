using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Vector3 = UnityEngine.Vector3;

public class BulletPool : MonoBehaviour
{
    [SerializeField, Header("玉")] private GameObject Bullet_;
    private ObjectPool<GameObject> BulletPool_;
    [SerializeField, Header("玉の最大数")] private int MaxBulletcount_ = 10;
    void Start()
    {
        //オブジェクトプール作成
        BulletPool_ = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(Bullet_, transform), // 1. 生成
            actionOnGet: (obj) => obj.SetActive(true),              // 2. 取得時（表示）
            actionOnRelease: (obj) => obj.SetActive(false),         // 3. 返却時（非表示）
            actionOnDestroy: (obj) => Destroy(obj),                 // 4. 破棄時
            collectionCheck: true, // 重複返却時のエラーチェック
            defaultCapacity: 5,    // 初期容量
            maxSize: MaxBulletcount_           // 最大容量
        );
    }

    /// <summary>
    /// バレット取得関数
    /// </summary>
    /// <returns></returns>
    public GameObject GetBullet() { return BulletPool_.Get(); }
    
    /// <summary>
    /// バレット変換関数
    /// </summary>
    /// <param name="obj"></param>
    public void ReleseBullet(GameObject obj) { BulletPool_.Release(obj); }
}
