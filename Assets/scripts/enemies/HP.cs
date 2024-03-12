using UnityEngine;

public class Enemy_01 : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private int meleeDamage;
    private bool isBoss = false;

    public static int enemiesDefeated = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void DealDamage(GameObject target)
    {
        Enemy_01 enemyHealth = target.GetComponent<Enemy_01>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(meleeDamage);
        }
    }

    void Die()
    {
        /*if (isBoss)
        {
            Boss.BossSpawnConditions.NotifyBossSpawned();
        }
        */
        Destroy(gameObject);
    }

    public void SetAsBoss()
    {
        //isBoss = true;
        maxHealth = 100;
        currentHealth = maxHealth;
    }
}
