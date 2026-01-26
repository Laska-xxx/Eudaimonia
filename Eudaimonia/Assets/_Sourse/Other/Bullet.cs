using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float liveTime = 5;

    private Vector3 _direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Transform pos, Vector3 direction)
    {
        gameObject.transform.position = pos.position;
        _direction = direction.normalized;
        transform.forward = _direction;

        gameObject.SetActive(true);
        StartCoroutine(BulletMove());
    }

    private IEnumerator BulletMove()
    {
        float curLieTime = 0;

        while (curLieTime < liveTime)
        {
            rb.linearVelocity = _direction * speed;
            yield return new WaitForSeconds(0.1f);
            curLieTime += 0.1f;
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
