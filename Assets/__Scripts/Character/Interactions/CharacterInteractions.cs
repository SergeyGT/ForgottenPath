using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IInteractableObject gameObject = other.GetComponent<IInteractableObject>();
        if (other != null)
        {
            gameObject.Interact();
        }
        else Debug.LogWarning("It`s not interactable object");
    }
}
