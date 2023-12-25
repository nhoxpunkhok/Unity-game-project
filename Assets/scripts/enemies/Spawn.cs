using System.Collections;
using UnityEngine;
using static Boss;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    private GameObject player;
    public float spawnDistance = 20f;
    public float minSpawnDistance = 10f;
    public float spawnInterval = 3f;
    private bool bossSpawned = false;

    private float BossMoveSpeed = 5f;

    void Start()
    {
        BossSpawnConditions.ResetConditions();
        BossSpawnConditions.BossSpawned += SpawnBoss;
        StartCoroutine(SpawnBossAfterDelay(30f));
        InvokeRepeating("SpawnRandomEnemies", spawnInterval, spawnInterval);
    }

    void SpawnRandomEnemies()
    {
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount >= BossSpawnConditions.maxEnemies || bossSpawned)
        {
            return;
        }

        int numEnemiesToSpawn = Random.Range(1, 4);
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            //  spawn enemy la boss -> dat la boss + dky su kien BossSpawned
            if (BossSpawnConditions.ShouldSpawnBoss())
            {
                enemy.GetComponent<Enemy_01>().SetAsBoss();
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        boss.GetComponent<Boss>().InitializeBoss(player.transform);
        bossSpawned = true;
    }

    IEnumerator SpawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBoss();
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
        float randomSpawnDistance = Random.Range(minSpawnDistance, spawnDistance);
        Vector3 spawnPosition = playerPosition + randomDirection.normalized * randomSpawnDistance;

        return spawnPosition;
    }
}
