using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance = Mathf.Infinity;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private ParticleSystem particle;

    private ParticleSystem curParticle;

    public void PerformShoot()
    {
        print("Shoot");
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            print(hitInfo.collider.name);
            var hitCollider = hitInfo.collider;

            if (hitCollider.TryGetComponent<AHealth>(out AHealth health))
            {
                curParticle = bloodParticle;
                
                health.GetDamage(damage);
            }
            else
            {
                particle.GetComponent<Renderer>().material = hitCollider.GetComponent<Renderer>().material;
                curParticle = particle;
            }

            curParticle.gameObject.transform.position = hitInfo.point;
            curParticle.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray,out RaycastHit hitInfo, distance, layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * distance;
            DrawRay(ray, hitPosition, distance, Color.green);
        }
    }

    private static void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.15f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
}
