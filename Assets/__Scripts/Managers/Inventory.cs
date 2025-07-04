using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _countBatteries = 0;
    private Dictionary<int, KeysSO> keyValuePairs = new Dictionary<int, KeysSO>();

    public static Inventory Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
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
        if (key == null)
        {
            Debug.LogError("Tried to add a null key to inventory!");
            return;
        }

        keyValuePairs[key.id] = key;
        Debug.Log(key.id);
        //UIManager.Instance.UpdateInventory(key.id);
    }

    public bool IsKeyInventory(int keyID)
    {
        return keyValuePairs.ContainsKey(keyID);
    }


}
