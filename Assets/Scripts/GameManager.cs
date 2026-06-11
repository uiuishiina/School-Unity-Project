using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("BulletPool")] private BulletPool pool;
    private List<GameObject> ActiveList = new List<GameObject>();
    [Header("初期地点")]
    [SerializeField,Tooltip("左")] private Vector3 left;
    [SerializeField, Tooltip("右")] private Vector3 right;
    [SerializeField, Tooltip("クールタイム")] float CoolTime_ = 2;
    [SerializeField, Tooltip("スピード")] int speed = 2;
    [SerializeField, Tooltip("ライフタイム")] float lieftime = 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int score_ = 0;

    float timer = 0;
    bool is_stop = false;

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (is_stop) {
            return;
        }
        
        timer += Time.deltaTime;
        if(timer > CoolTime_) {
            timer = 0;
            var g = pool.GetBullet();
            
            if (UnityEngine.Random.Range(0, 2) == 0) {
                g.GetComponent<Bullet>().Initialize(left, speed * 1, lieftime, BulletReleace);
            }
            else {
                g.GetComponent<Bullet>().Initialize(right, speed * -1, lieftime, BulletReleace);
            }
            ActiveList.Add(g);
        }
    }

    void ResetGame()
    {
        is_stop = false;
        score_ = 0;
        ScoreUpdate(0);
    }
    public void ScoreUpdate(int score)
    {
        score_ += score;
        text.text = "Score :" + score_.ToString();
    }

    void BulletReleace(GameObject gameObject)
    {
        ActiveList.Remove(gameObject);
        pool.ReleseBullet(gameObject);
    }
    void AllReleace()
    {
        var list = new List<GameObject>(ActiveList);

        foreach (var obj in list)
        {
            ExecuteEvents.Execute<RecieveInterface>(
                target: obj,
                eventData: null,
                functor: (receiver, eventData) => receiver.OnRecieve());
        }
    }
    /// <summary>
    /// プレイヤー死亡時処理関数
    /// </summary>
    /// <param name="action"></param>
    public async void Death_player(Action action)
    {
        is_stop = true;
        AllReleace();
        await UniTask.Delay(3000);
        action();
        ResetGame();
    }
}
