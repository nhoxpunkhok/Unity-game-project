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
        PerformMeleeAttack();
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
                Debug.Log("Melee attack performed at: " + Time.time);
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

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, outerRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        DrawWireArc(transform.position, transform.up, transform.forward, attackAngle, attackRange, Color.red);
#endif
    }

#if UNITY_EDITOR
    void DrawWireArc(Vector3 position, Vector3 axis, Vector3 from, float angle, float radius, Color color)
    {
        Handles.color = color;
        Handles.DrawWireArc(position, axis, from, angle, radius);
    }
#endif

    void DealDamage(GameObject target)
    {
        Enemy_01 enemyHealth = target.GetComponent<Enemy_01>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(meleeDamage);
        }
    }
}
