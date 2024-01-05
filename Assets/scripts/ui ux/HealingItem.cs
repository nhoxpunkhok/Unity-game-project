using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealingItem : MonoBehaviour
{
    public float healAmount = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Stats>().HealPlayer(healAmount);
            Destroy(gameObject);
        }
         
    }
}