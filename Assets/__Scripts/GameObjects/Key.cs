using System;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteractableObject
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private KeysSO key;

    private MeshRenderer keyRenderer;

    public static event Action<KeysSO> pickedUp;

    private void Start()
    {
        keyRenderer = GetComponent<MeshRenderer>();
        keyRenderer.material = defaultMaterial;
    }

    public void Highlight(bool isHighlighted)
    {
        if (keyRenderer == null) return;

        keyRenderer.material = isHighlighted ? highlightMaterial : defaultMaterial;
    }

    public void Interact()
    {
        print("Pickup key");
        pickedUp?.Invoke(key);
        Destroy(gameObject);
    }
    public int GetId() => key.id;
    public string GetTitle() => key.title;
}
