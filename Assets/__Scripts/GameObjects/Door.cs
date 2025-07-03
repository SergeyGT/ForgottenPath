using System;
using UnityEngine;

public abstract class Door : MonoBehaviour, IInteractableObject
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Boolean isDoorWithKey;
    [SerializeField] private string doorName;

    private MeshRenderer doorRenderer;
    
    private Boolean isDoorOpen;

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

    }

    private void CloseDoor()
    {

    }
}
