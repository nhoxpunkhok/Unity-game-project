using DA_Assets.FCU.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f; // 1_lan/2s
    private float nextFireTime = 0f;
    public static float BulletSpeed = 20f;
    public static int BulletDamage = 10;
    public static float BulletRange = 30f;

    void Update()
    {
        // kiem soat toc do ban
        if (Time.time >= nextFireTime)
        {
            ShootAtNearestEnemy();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtNearestEnemy()
    {
        // ds chua cac tag 
        string[] targetTags = { "Enemy", "Boss" };

        // tim cac doi tuong co trong tag
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTags[0]);

        for (int i = 1; i < targetTags.Length; i++)
        {
            GameObject[] additionalObjects = GameObject.FindGameObjectsWithTag(targetTags[i]);
            targetObjects = targetObjects.Concat(additionalObjects).ToArray();
        }

        if (targetObjects.Length > 0)
        {
            GameObject nearestTarget = targetObjects[0];
            float nearestDistance = Vector3.Distance(transform.position, nearestTarget.transform.position);

            foreach (GameObject target in targetObjects)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);

                // kiem tra ke dich co trong pham vi ban khong
                if (distance <= BulletRange && distance < nearestDistance)
                {
                    nearestTarget = target;
                    nearestDistance = distance;
                }
            }

            // kiem tra co ke dich gan nhat khong
            if (nearestTarget != null)
            {
                // xac dinh huong den cua ke dich gan nhat ( dung hon la huong bay cua vien dan)
                Vector3 direction = (nearestTarget.transform.position - transform.position).normalized;

                // tao ban sao vien dan sau khi ban 
                GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 1.5f, Quaternion.LookRotation(direction));
                // gan parent cho dan
                bullet.transform.SetParent(null);
                // tham so cho dan trong BulletScript
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                if (bulletScript != null)
                {
                    bulletScript.SetParameters(BulletSpeed, BulletDamage, BulletRange);
                }
            }
        }
    }
}
