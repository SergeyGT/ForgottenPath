using System;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
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
