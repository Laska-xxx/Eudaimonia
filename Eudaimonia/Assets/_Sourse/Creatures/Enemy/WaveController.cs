using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private int baseEnemyCount = 10;
    [SerializeField] private float enemyCountRatio = 1.2f;
    [SerializeField] private float waveLength = 60f;
    [SerializeField] private float waveLenghtRatio = 1.1f;
    [SerializeField] private float waveCooldown = 5;
    [SerializeField] private float spawnEnemyCooldown = 2f;
    [SerializeField] private SpawnEnemy spawnEnemy;

    private int countEnemySpawn;
    private int countEnemyPerSpawn;

    private void Awake()
    {
        StartWave();
    }

    private void StartWave()
    {
        countEnemySpawn = Mathf.FloorToInt(waveLength / spawnEnemyCooldown);
        countEnemyPerSpawn = Mathf.CeilToInt(baseEnemyCount / countEnemySpawn);
        if (countEnemyPerSpawn < 1 ) 
            countEnemyPerSpawn = 1;

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < countEnemySpawn; i++)
        {
            print("start spawn");
            spawnEnemy.Spawn(countEnemyPerSpawn);
            yield return new WaitForSeconds(spawnEnemyCooldown);
        }
    }

    private IEnumerator Wave()
    {
        yield return new WaitForSeconds(waveLength);

        StartCoroutine(WaveCooldown());
    }

    private IEnumerator WaveCooldown()
    {
        yield return new WaitForSeconds(waveCooldown);
        baseEnemyCount = Mathf.CeilToInt(baseEnemyCount * enemyCountRatio);
        waveLength = Mathf.CeilToInt(waveLength * waveLenghtRatio);
        StartWave();
    }
}