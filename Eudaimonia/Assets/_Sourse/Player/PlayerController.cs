using Source.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float dashForce = 25f;
    [SerializeField] private float dashFadeDuration = 0.3f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private readonly float gravity = -9.81f;
    [SerializeField] private CharacterController character;

    private Vector3 _velocity;
    private Vector3 _curVelocity;
    private bool _isDash = false;
    private bool _canDash = true;
    
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;

    private void OnEnable()
    {
        _moveAction = InputManager.Instance.GameInput.Player.Move;
        _jumpAction = InputManager.Instance.GameInput.Player.Jump;
        _dashAction = InputManager.Instance.GameInput.Player.Dash;

        _jumpAction.performed += Jump;
        _dashAction.performed += Dash;
    }

    private void OnDisable()
    {
        _jumpAction.performed -= Jump;
        _dashAction.performed -= Dash;
    }

    void Start()
    {
        _curVelocity = Vector3.zero;
    }

    void Update()
    {
        HandleGravity();

        if (!_isDash)
        {
            HandleMovement();
        }

        ApplyMovement();
    }

    private void HandleGravity()
    {
        if (character.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;
    }

    private void HandleMovement()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;

        Vector3 targetVelocity = moveDirection * speed;
        _curVelocity = Vector3.Lerp(_curVelocity, targetVelocity, 10f * Time.deltaTime);

        if (input.magnitude < 0.1f)
        {
            _curVelocity = Vector3.Lerp(_curVelocity, Vector3.zero, 15f * Time.deltaTime);
        }
    }

    private void ApplyMovement()
    {
        Vector3 totalVelocity = new Vector3(_curVelocity.x, _velocity.y, _curVelocity.z);
        character.Move(totalVelocity * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (character.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void Dash(InputAction.CallbackContext ctx)
    {
        if (!_canDash) return;

        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 dashDirection;

        if (input.magnitude > 0.1f)
        {
            dashDirection = (transform.right * input.x + transform.forward * input.y).normalized;
        }
        else
        {
            dashDirection = transform.forward;
        }

        StartCoroutine(DashCoroutine(dashDirection));
    }

    private IEnumerator DashCoroutine(Vector3 direction)
    {
        _isDash = true;
        _canDash = false;

        Vector3 dashImpulse = direction * dashForce;
        _curVelocity = dashImpulse;

        yield return null;

        float fadeTimer = 0f;
        Vector3 startVelocity = _curVelocity;

        while (fadeTimer < dashFadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / dashFadeDuration;

            Vector2 input = _moveAction.ReadValue<Vector2>();
            Vector3 targetMoveDirection = transform.right * input.x + transform.forward * input.y;
            Vector3 targetVelocity = targetMoveDirection * speed;

            _curVelocity = Vector3.Lerp(startVelocity, targetVelocity, t);

            yield return null;
        }

        _isDash = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }
}
