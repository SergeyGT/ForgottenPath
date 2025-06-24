using System;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractableObject
{
    [Header("Battery Settings")]
    [SerializeField] private float _capacity = 40;

    //public static Action<float> GiveCharge;

    public void Interact()
    {
        //GiveCharge?.Invoke(_capacity);
        Inventory.Instance.SetBatteries(1);
        Destroy(gameObject);
    }

    public void Highlight()
    {

    }
}
