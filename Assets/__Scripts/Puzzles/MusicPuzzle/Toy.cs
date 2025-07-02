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
    }

    public void Interact()
    {
        SFXManager.Instance.PlaySound(note, transform, 0.3f);
        ToyPuzzle.Instance.Entered(number);
    }
}
