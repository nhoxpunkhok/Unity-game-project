using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static class BossSpawnConditions
    {
        public static event System.Action BossSpawned;

        public static int enemiesPerWave = 5;
        public static int maxEnemies = 10;
        private static int enemiesDefeatedForBoss = 20;

        public static void ResetConditions()
        {
            Enemy_01.enemiesDefeated = 0;
        }

        public static bool ShouldSpawnBoss()
        {
            return Enemy_01.enemiesDefeated >= enemiesDefeatedForBoss;
        }

        public static void NotifyBossSpawned()
        {
            BossSpawned?.Invoke();
        }
    }
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    private bool isBoss = false;
    private Transform playerTransform;
    public static float BossMoveSpeed = 5f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        //Boss di chuyen ve phia (Player)
        if (isBoss && playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * BossMoveSpeed);
        }
    }

    public void InitializeBoss(Transform playerTransform)
    {
        isBoss = true;
        maxHealth = 100;
        currentHealth = maxHealth;
        this.playerTransform = playerTransform;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isBoss)
        {
            BossSpawnConditions.NotifyBossSpawned();
        }

        Destroy(gameObject);
    }
}
