using Unity.Cinemachine;
using UnityEngine;

public class FPVCharacter : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float rotationX = 0f;
    private float rotationY = 0f;
    [SerializeField] private Transform player;

    public float sensivity;

    private void Start()
    {
        if (sensivity == null || sensivity == 0) sensivity = 1;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity;

        //rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cinemachineCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        player.Rotate(Vector3.up * mouseX);
    }
}
