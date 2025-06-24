using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    [SerializeField] private float maxDistanceToInteract;

    public static Action GiveCharge;

    private void OnTriggerEnter(Collider other)
    {
        IInteractableObject gameObject = other.GetComponent<IInteractableObject>();
        if (other != null)
        {
            gameObject.Interact();
        }
        else Debug.LogWarning("It`s not interactable object");
    }

    private void Update()
    {
        ChargeFlashlight();
        CheckInventory();

        Debug.DrawRay(transform.position, transform.forward * maxDistanceToInteract, Color.red);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistanceToInteract))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                IInteractableObject door = hit.collider.gameObject.GetComponent<IInteractableObject>();
                door.Interact();
            }
        }

    }

    private void ChargeFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Inventory.Instance.GetBatteries() > 0)
            {
                Inventory.Instance.SetBatteries(-1);
                GiveCharge?.Invoke();
            }
            else
            {
                UIManager.Instance.ShowElement("batteryEnded");
                UIManager.Instance.HideElement("batteryEnded");
            }
        }
    }

    private void CheckInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.StartClosingEyes();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.Instance.StartOpeningEyes();
        }
    }

    
}
