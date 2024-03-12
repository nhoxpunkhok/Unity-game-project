using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;

    NavMeshAgent agent;
    Animator animator;

    float timer = 0.0f; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f){
            float sqDistance = (player.position - transform.position).sqrMagnitude;
            if (sqDistance < maxDistance*maxDistance) { 
                agent.destination = player.position;
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
