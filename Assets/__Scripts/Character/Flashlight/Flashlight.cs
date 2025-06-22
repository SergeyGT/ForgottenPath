using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;    
    private bool isActive = false;

    [Header("Flashlight Settings")]
    [SerializeField] private float _capacity = 100f;
    [SerializeField] private float _chargeTime = 0.5f;

    private const float ACUM_SIZE = 100f;
    
    private void Start()
    {
        if (flashlight == null) Debug.LogError("Flashlight is NULL!");
    }

    private void OnEnable()
    {
        Battery.GiveCharge += TakeCharge;
    }

    private void OnDisable()
    {
        Battery.GiveCharge -= TakeCharge;
    }

    private void TakeCharge(float amount)
    {
        _capacity += amount;
        _capacity = Mathf.Min(_capacity, ACUM_SIZE);
    }

    private void Update()
    {
        // не включаем фонарь если заряда нет
        if (Input.GetKeyDown(KeyCode.Mouse1) && _capacity != 0)
        {
            isActive = !isActive;

            flashlight.SetActive(isActive);
        }

        // если фонарь работал, но заряда больше нет, он выкючается автоматически
        if (_capacity == 0)
        {
            flashlight.SetActive(false);
        }

    }
}
