using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;    
    private bool isActive = false;

    [Header("Flashlight Settings")]
    [SerializeField] private float _capacity = 100f;
    [SerializeField] private float _chargeTakes = 0.5f;

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
        // �� �������� ������ ���� ������ ���
        if (Input.GetKeyDown(KeyCode.Mouse1) && _capacity != 0)
        {
            isActive = !isActive;

            flashlight.SetActive(isActive);
        }

        // ���� ������ �������, �� ������ ������ ���, �� ���������� �������������
        if (_capacity <= 0)
        {
            flashlight.SetActive(false);
        }
        else InvokeRepeating("GiveBatteryCharge", 2, 2);
    }

    private void GiveBatteryCharge()
    {
        _capacity -= _chargeTakes;
        if (_capacity < 0)
        {
            _capacity = 0;
        }
    }
}
