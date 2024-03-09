using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash : MonoBehaviour
{
    Movement moveScript;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private bool canDash = true;


    private void Start()
    {
        moveScript = GetComponent<Movement>();
    }
    private void Update()
    {     
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dashes());
        }

    }
    IEnumerator Dashes()
    {
        canDash = false;
       
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    } 
 
}