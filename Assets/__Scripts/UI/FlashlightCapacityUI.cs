using UnityEngine;
using UnityEngine.UI;

public class FlashlightCapacityUI : MonoBehaviour
{
    [Header("Settings Capacity Flashlight")]
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private Image image;

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

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        image.fillAmount = flashlight.GetCapacity() / flashlight.GetAcumSize();
        if(flashlight.GetCapacity() <= 20) image.color = Color.red;
        else image.color = Color.yellow;
    }
}
