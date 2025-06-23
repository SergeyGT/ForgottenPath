using UnityEngine;
using UnityEngine.UI;

public class FlashlightCapacityUI : MonoBehaviour
{
    [Header("Settings Capacity Flashlight")]
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Image image;

    private Flashlight flashlightActions;
    private void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("Flashlight is NULL!!!");
            enabled = false;
        }

        if(image == null)
        {
            Debug.LogError("imageUI is NULL!!!");
            enabled = false;
        }

        flashlightActions = flashlight.GetComponent<Flashlight>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        image.fillAmount = flashlightActions.GetCapacity() / flashlightActions.GetAcumSize();
        if(flashlightActions.GetCapacity() <= 20) image.color = Color.red;
        else image.color = Color.yellow;
    }
}
