using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private void Start()
    {
        
    }

    /// <summary>
    /// ƒvƒŒƒCƒ„[€–Sˆ—ŠÖ”
    /// </summary>
    /// <param name="action"></param>
    public async void Death_player(Action action)
    {
        await UniTask.Delay(3000);
        action();
    }
}
