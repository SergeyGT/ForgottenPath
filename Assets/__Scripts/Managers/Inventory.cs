using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int _countBatteries = 0;
    private Dictionary<string, Key> keyValuePairs = new Dictionary<string, Key>();

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

    public void SetBatteries(int countBatteries)
    {
        _countBatteries += countBatteries;
    }

    public int GetBatteries()
    {
        return _countBatteries;
    }

    public void SetKey(string key, Key obj)
    {
        keyValuePairs[key] = obj;
    }

    public bool GetKey(string key)
    {
        return keyValuePairs[key];
    }

}
