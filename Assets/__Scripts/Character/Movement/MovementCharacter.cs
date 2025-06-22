using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _shiftSpeed = 5;
    [SerializeField] private float _acceleration = 0.2f;
    [SerializeField] private float _deceleration = 0.1f;
    private Rigidbody _rigidbody;
    private Vector3 _currentVelocity;
    private Vector3 _targetVelocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * verticalMove
                         + transform.right * horizontalMove;
        move.y = 0;
        move = move.normalized;

        if (Input.GetKey(KeyCode.LeftShift)) _targetVelocity = move * _shiftSpeed;
        else _targetVelocity = move * _speed;
    }

    private void FixedUpdate()
    {
        float lerpTime = (_targetVelocity == Vector3.zero) ? _deceleration : _acceleration;
        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, Time.fixedDeltaTime / lerpTime);

        _rigidbody.linearVelocity = new Vector3(_currentVelocity.x, _rigidbody.linearVelocity.y, _currentVelocity.z);
    }
}
