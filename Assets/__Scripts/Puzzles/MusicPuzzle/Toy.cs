using System;
using UnityEngine;

public class Toy : MonoBehaviour, IInteractableObject
{
    [Header("Set Textures Toy")]
    [SerializeField] private Material defaulMaterial;
    [SerializeField] private Material highlightedMaterial;

    [Header("Params of toy")]
    [SerializeField] private int number;
    [SerializeField] private AudioClip note;
   
    public Action<int> pressed;

    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material = defaulMaterial;
    }

    public void Highlight(bool isHighlighted)
    {
        if (highlightedMaterial == null) return;

        renderer.material = isHighlighted ? highlightedMaterial : defaulMaterial;

        print("Toy highlighted");
    }

    public void Interact()
    {
        SFXManager.Instance.PlaySound(note, transform, 1f);
        pressed?.Invoke(number);
    }
}
