using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    private int _countBatteries = 0;
    private Dictionary<int, KeysSO> keyValuePairs = new Dictionary<int, KeysSO>();

    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Inventory();
            return _instance;
        }
    }

    private void OnEnable()
    {
        Key.pickedUp += SetKey;
    }
    private void OnDisable()
    {
        Key.pickedUp -= SetKey;
    }

    public void SetBatteries(int countBatteries)
    {
        _countBatteries += countBatteries;
        UIManager.Instance.UpdateInventory();
    }

    public int GetBatteries()
    {
        return _countBatteries;
    }

    public void SetKey(KeysSO key)
    {
        keyValuePairs[key.id] = key;
        UIManager.Instance.UpdateInventory(key.id);
    }

    public bool IsKeyInventory(int keyID)
    {
        return keyValuePairs[keyID];
    }

}
