using UnityEngine;

public class Door : MonoBehaviour, IInteractableObject
{
    private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    private MeshRenderer doorRenderer;

    private void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = doorRenderer.material;
    }

    public void Interact()
    {
        Debug.Log("Door opened!");
        // Логика открытия двери
    }

    public void Highlight(bool isHighlighted)
    {
        if (doorRenderer == null) return;

        doorRenderer.material = isHighlighted ? doorRenderer.materials[1] : doorRenderer.materials[0];

        Debug.Log(isHighlighted ? "Door highlighted!" : "Highlight removed");
    }
}
