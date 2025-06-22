using System;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractableObject
{
    [Header("Battery Settings")]
    [SerializeField] private float _capacity = 40;

    public static Action<float> GiveCharge;

    public void Interact()
    {
        GiveCharge?.Invoke(_capacity);
        Destroy(gameObject);
    }
}
