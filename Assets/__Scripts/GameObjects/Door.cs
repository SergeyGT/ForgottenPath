using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;

public class Door : MonoBehaviour, IInteractableObject
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Boolean isDoorWithKey;
    [SerializeField] private string doorName;
    [SerializeField] private List<Lock> locks;

    private MeshRenderer doorRenderer;
    
    private bool isDoorOpenWithKeys = false;
    private bool isDoorOpen = false;

    [System.Serializable]
    public class Lock
    {
        public int number;
        public bool isUnlocked = false;
    }

    private void Awake()
    {
        isDoorOpen = false;
    }
    private void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        doorRenderer.material = defaultMaterial;
    }

    public void Interact()
    {
        if (isDoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    public void Highlight(bool isHighlighted)
    {
        if (doorRenderer == null) return;

        doorRenderer.material = isHighlighted ? highlightMaterial : defaultMaterial;

        Debug.Log(isHighlighted ? "Door highlighted!" : "Highlight removed");
    }

    private void OpenDoor()
    {
        if (isDoorWithKey && !isDoorOpenWithKeys)
        {
            for(int i = 0; i<locks.Count; i++)
            {
                if (Inventory.Instance.IsKeyInventory(locks[i].number))
                {
                    locks[i].isUnlocked = true;
                    isDoorOpenWithKeys = true;
                }
                else
                {
                    isDoorOpenWithKeys = false;
                    Debug.LogWarning("Не все ключи собраны");
                    break;
                }
            }

            if (!isDoorOpenWithKeys)
            {
                Debug.LogWarning("Дверь закрыта");
                return;
            }  
        }
        // Анимация открытия двери
        isDoorOpen = true;
    }

    private void CloseDoor()
    {
        //анимация закрытия двери
        isDoorOpen = false;
    }
}
