using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteractableObject
{
    [SerializeField] private KeysSO key;

    public static UnityAction<KeysSO> pickedUp;
    public void Highlight(bool isHighlighted)
    {
        throw new System.NotImplementedException();
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
