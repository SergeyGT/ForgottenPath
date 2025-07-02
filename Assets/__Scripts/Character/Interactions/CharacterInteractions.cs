using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    [SerializeField] private float maxDistanceToInteract;
    [SerializeField] private LayerMask layerMaskInteractableObjects;

    private IInteractableObject lastInteractableObject;

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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceToInteract, layerMaskInteractableObjects))
        {
            lastInteractableObject = hit.collider.gameObject.GetComponent<IInteractableObject>();
            lastInteractableObject.Highlight(true);
        }
        else
        {
            if (lastInteractableObject == null) return;
            lastInteractableObject.Highlight(false);
            lastInteractableObject = null;
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
