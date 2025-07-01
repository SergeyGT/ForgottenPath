using UnityEngine;

public class Door : MonoBehaviour, IInteractableObject
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    private MeshRenderer doorRenderer;

    private void Start()
    {
        doorRenderer = GetComponent<MeshRenderer>();
        doorRenderer.material = defaultMaterial;
    }

    public void Interact()
    {
        Debug.Log("Door opened!");
        // Логика открытия двери
    }

    public void Highlight(bool isHighlighted)
    {
        if (doorRenderer == null) return;

        doorRenderer.material = isHighlighted ? highlightMaterial : defaultMaterial;

        Debug.Log(isHighlighted ? "Door highlighted!" : "Highlight removed");
    }
}
