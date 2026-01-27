using Source.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private RayCastShoot rayCastShoot;

    private bool _canShoot = true;
    private InputAction _ShootAction;

    private void OnEnable()
    {
        _ShootAction = InputManager.Instance.GameInput.Player.Attack;

        _ShootAction.performed += Shoot;
    }

    private void OnDisable()
    {
        _ShootAction.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (_canShoot)
        {
            rayCastShoot.PerformShoot();
            shootParticle.Play();
        }

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
}
