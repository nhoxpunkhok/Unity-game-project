#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public static int meleeDamage = 10;
    public float attackRange = 4f;
    public float outerRange = 4f;
    public LayerMask targetLayer;
    public float attackAngle = 45f;
    public float attackDelay = 3f;
    private float lastAttackTime;

    void Update()
    {
        //PerformMeleeAttack();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastAttackTime >= attackDelay)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("attack");
                //Debug.Log("Melee attack performed at: " + Time.time);
                lastAttackTime = Time.time ;
   
            }
        }
        else
        {
            Debug.Log("thoi gian den lan tan cong ke tip: " + (lastAttackTime + attackDelay - Time.time));
        }
    }
    void PerformMeleeAttack()
    {
        if (Time.time - lastAttackTime >= attackDelay)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, outerRange, targetLayer);

            // Attack the nearest enemy
            Collider nearestCollider = FindNearestEnemy(hitColliders);

            // Deal damage to all enemies within attack range
            DealDamageToAll(hitColliders);

            if (nearestCollider != null)
            {
                Vector3 directionToEnemy = nearestCollider.transform.position - transform.position;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                if (angleToEnemy < attackAngle)
                {
                    DealDamage(nearestCollider.gameObject);
                    lastAttackTime = Time.time;
                }
            
            }
        }
        else
        {
            Debug.Log("thoi gian den lan tan cong ke tip: " + (lastAttackTime + attackDelay - Time.time));
        }
    }




    // Find the nearest enemy among the given colliders
    Collider FindNearestEnemy(Collider[] colliders)
    {
        Collider nearestCollider = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                if (distanceToEnemy < nearestDistance)
                {
                    nearestDistance = distanceToEnemy;
                    nearestCollider = collider;
                }
            }
        }

        return nearestCollider;
    }

    // Deal damage to all enemies within attack range
    void DealDamageToAll(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                DealDamage(collider.gameObject);
            }
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
}
