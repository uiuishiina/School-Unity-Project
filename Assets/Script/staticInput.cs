
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class staticInput : staticObject<staticInput>
{
    private PlayerInput Input_;

    [SerializeField, Tooltip("使用しているマップ")] private InputActionMap EnableMap_;
    [SerializeField, Tooltip("デフォルトマップ名")] private string DefaultName_;
    Dictionary<string, InputActionMap> MapContainer_ = new Dictionary<string, InputActionMap>();
    protected override void Awake()
    {
        base.Awake();
        if (Instance_ != this){
            return;
        }
        Input_ = GetComponent<PlayerInput>();

        if (!AddActionMap(DefaultName_, out InputActionMap map)) {
            Debug.LogError("Not Find DefaultMap");
        }
        ChengeActionMap(DefaultName_);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnableMap_.Enable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EnableMap_.Disable();
    }

    public InputActionMap GetActionMap(string mapName)
    {
        if (MapContainer_.TryGetValue(mapName, out InputActionMap map)) {
            return map;
        }
        else {
            if (AddActionMap(mapName, out map)) {
                return map;
            }
            return null;
        }
    }

    public void ChengeActionMap(string mapName)
    {
        Input_.SwitchCurrentActionMap(mapName);
        Debug.Log($" New Map = {mapName}");
    }

    private bool AddActionMap(string mapName, out InputActionMap map)
    {
        map = Input_.actions.FindActionMap(mapName);
        if (map != null) {
            MapContainer_[mapName] = map;
            map.Disable();
            return true;
        }
        else {
            return false;
        }
    }
}