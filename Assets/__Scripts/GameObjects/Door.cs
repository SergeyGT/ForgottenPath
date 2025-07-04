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
    
    private Boolean isDoorOpen;

    [System.Serializable]
    public class Lock
    {
        [SerializeField] private int number;
        private bool isUnlocked;
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
        Debug.Log("Door opened!");
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
        if (isDoorWithKey && !isDoorOpen)
        {
                
        }
        else
        {

        }
    }

    private void CloseDoor()
    {

    }
}
