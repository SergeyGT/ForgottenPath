using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    private int _countBatteries = 0;
    private Dictionary<int, Key> keyValuePairs = new Dictionary<int, Key>();

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
        //Key.pickedUp += 
    }
    private void OnDisable()
    {

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

    public void SetKey(int keyID, Key obj)
    {
        keyValuePairs[keyID] = obj;
        UIManager.Instance.UpdateInventory(keyID);
    }

    public bool IsKeyInventory(int keyID)
    {
        return keyValuePairs[keyID];
    }

}
