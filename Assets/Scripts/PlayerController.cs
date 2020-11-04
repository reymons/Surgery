using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _speed = 20f;

    private float _cameraPitch = 0.0f;
    private CharacterController _cc;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdatePlayerLook();
        UpdatePlayerMovement();
    }

    private void UpdatePlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _cameraPitch -= mouseY * _mouseSensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -70f, 70);

        _mainCamera.localEulerAngles = Vector3.right * _cameraPitch;
        transform.Rotate(Vector3.up * mouseX * _mouseSensitivity);
    }

    private void UpdatePlayerMovement()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        Vector3 velocity = (transform.forward * direction.y + transform.right * direction.x) * _speed;
        _cc.Move(velocity * Time.deltaTime);
    }
}
