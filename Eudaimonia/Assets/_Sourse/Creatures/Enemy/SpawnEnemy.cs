using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<PatrulPoint> patrulPoints;

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            print("spawn");
            var enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position,
            Quaternion.identity, transform);
            print(enemy.name);
            enemy.GetComponent<EnemyMovement>().Init(patrulPoints, player);
            print("end spawn");
        }
    }
}
