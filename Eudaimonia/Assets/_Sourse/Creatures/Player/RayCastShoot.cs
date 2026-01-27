using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance = Mathf.Infinity;
    [SerializeField] private BloodPoolController bloodPoolController;

    public void PerformShoot()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            var hitCollider = hitInfo.collider;

            if (hitCollider.TryGetComponent<AHealth>(out AHealth health))
            {
                var blood = bloodPoolController.GetParticle();
                blood.gameObject.transform.position = hitInfo.point;
                blood.Play();
                health.GetDamage(damage);
            }
        }
    }
}
