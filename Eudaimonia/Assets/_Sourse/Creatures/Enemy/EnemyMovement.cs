using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5;
    [SerializeField] private float stalkSpeed = 10;
    [SerializeField] private float patrolCooldown = 3;
    [SerializeField] private float stalkDuration = 3;
    [SerializeField] private float stalkCooldown = 3;
    [SerializeField] private List<PotrulPoint> points;
    [SerializeField] private Transform player;
    [SerializeField] private NearPlayerTrigger detectPlayerTrigger;
    [SerializeField] private NearPlayerTrigger nearPlayerTrigger;
    private NavMeshAgent agent;
    private PotrulPoint curPoint;
    private bool isStalking = false;
    private bool canStalking = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Patrol();
    }

    private void Update()
    {
        if (isStalking)
        {
            return;
        }
        if (detectPlayerTrigger.IsPlayerNearby && canStalking)
        {
            isStalking = true;
            canStalking = false;
            StartCoroutine(Stalk());
            return;
        }
        if (Vector2.Distance(PosXZ(agent.transform.position), 
            PosXZ(curPoint.gameObject.transform.position)) <= 0.5f)
        {
            curPoint.IsUse = false;
            StartCoroutine(PatrolCooldown());
        }
    }

    private void Patrol()
    {
        int randPointIndex = Random.Range(0, points.Count);

        while (points[randPointIndex].IsUse == true)
        {
            randPointIndex = Random.Range(0, points.Count);
        }

        curPoint = points[randPointIndex];

        agent.SetDestination(curPoint.gameObject.transform.position);
    }

    private IEnumerator Stalk()
    {
        agent.speed = stalkSpeed;
        float curDuration = stalkDuration;
        float step = 0.1f;

        while (curDuration > 0)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(step);
            curDuration -= step;

        }

        if (nearPlayerTrigger.IsPlayerNearby)
        {
            StartCoroutine(Stalk());
        }
        else
        {
            isStalking = false;
            agent.speed = baseSpeed;
            StartCoroutine(StalkCooldown());
            Patrol();
        }
        
    }

    private IEnumerator StalkCooldown()
    {
        yield return new WaitForSeconds(stalkCooldown);
        canStalking = true;
    }

    private IEnumerator PatrolCooldown()
    {
        yield return new WaitForSeconds(patrolCooldown);
        Patrol();
    }

    private Vector2 PosXZ(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}
