
using System.Collections.Generic;
using UnityEngine;

public class staticObjectFactory : MonoBehaviour
{
    [Header("共通のSceneManager")]
    [SerializeField] private staticSceneManager sceneManager_;
    [Header("生成するオブジェクトリスト")]
    [SerializeField] private List<staticObjectBase> staticObjectList_ = new();
    private void Awake()
    {
        //  シーンマネージャー生成
        Instantiate(sceneManager_);
        //  各StaticObj生成
        foreach (var obj in staticObjectList_)
        {
            Instantiate(obj);
        }
    }
}