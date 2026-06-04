using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("BulletPool")] private BulletPool pool;
    [Header("初期地点")]
    [SerializeField,Tooltip("左")] private Vector3 left;
    [SerializeField, Tooltip("右")] private Vector3 right;
    [SerializeField, Tooltip("クールタイム")] float CoolTime_ = 2;
    [SerializeField, Tooltip("スピード")] int speed = 2;
    [SerializeField, Tooltip("ライフタイム")] float lieftime = 2;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > CoolTime_) {
            timer = 0;
            var g = pool.GetBullet();
            
            if (UnityEngine.Random.Range(0, 2) == 0) {
                g.GetComponent<Bullet>().Initialize(left, speed * 1, lieftime, pool.ReleseBullet);
            }
            else {
                g.GetComponent<Bullet>().Initialize(right, speed * -1, lieftime, pool.ReleseBullet);
            }
        }
    }

    /// <summary>
    /// プレイヤー死亡時処理関数
    /// </summary>
    /// <param name="action"></param>
    public async void Death_player(Action action)
    {
        await UniTask.Delay(3000);
        action();
    }
}
