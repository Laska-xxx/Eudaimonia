using Source.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private CharacterController character;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    private bool _isGround;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private void OnEnable()
    {
        _moveAction = InputManager.Instance.GameInput.Player.Move;
        _jumpAction = InputManager.Instance.GameInput.Player.Jump;

        _jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        _moveAction = null;

        _jumpAction.performed -= Jump;
    }

    void Update()
    {
        if (character.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Move();

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 moveVector = transform.right * _moveAction.ReadValue<Vector2>().x + transform.forward * _moveAction.ReadValue<Vector2>().y;
        character.Move(moveVector * speed);
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        print(character.isGrounded);
        if (character.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        print(velocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundCheckDistance);
    }
}
