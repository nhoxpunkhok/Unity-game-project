using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed;
    private int damage;
    private float range;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // gravity OFF
    }

    void Update()
    {
        // check range max
        if (transform.parent != null && Vector3.Distance(transform.position, transform.parent.position) > range)
        {
            Destroy(gameObject);
        }
    }

    // tham so cho dan
    public void SetParameters(float newSpeed, int newDamage, float newRange)
    {
        speed = newSpeed;
        damage = newDamage;
        range = newRange;
    }

    void FixedUpdate()
    {
        // Di chuyen theo huong dc ban ra
        Vector3 velocity = transform.forward * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        Debug.Log("BulletScript - Current Velocity: " + velocity.magnitude);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // gay sat thuong
            Enemy_01 enemy = collision.gameObject.GetComponent<Enemy_01>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // huy dan khi va cham voi enemy
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            // dan bien mat neu cham doi tuong ngoai player
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
