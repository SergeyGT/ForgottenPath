using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    [SerializeField] private int _speed = 1;
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

        Vector3 move = new Vector3(horizontalMove, 0, verticalMove).normalized;
        _targetVelocity = move * _speed;
    }

    private void FixedUpdate()
    {
        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, Time.fixedDeltaTime / 0.2f);

        Vector3 movement = _currentVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }
}
