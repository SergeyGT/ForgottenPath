using UnityEngine;

public class Door : MonoBehaviour, IInteractableObject
{
    public void Interact()
    {
        Debug.Log("Check door");
    }

    public void Highlight()
    {

    }
}
