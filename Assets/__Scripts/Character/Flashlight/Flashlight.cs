using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    private bool isActive = false;
    private void Start()
    {
        if (flashlight == null) Debug.LogError("Flashlight is NULL!");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isActive = !isActive;

            flashlight.SetActive(isActive);
        }

    }
}
