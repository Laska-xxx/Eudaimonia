using Source.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private Transform shootPos;
    [SerializeField] private BulletPoolController bulletPoolController;

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var dir = ray.direction;
            dir.z += 0.05f;
            dir.y += 0.05f;
            bulletPoolController.GetBullet().Init(shootPos, dir);
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
