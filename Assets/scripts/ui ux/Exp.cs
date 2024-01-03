using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Exp : MonoBehaviour
{
    public float expAmount = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerExp>().ExpPlayer(expAmount);
            Destroy(gameObject);
        }
         
    }
}