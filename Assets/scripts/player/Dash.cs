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

    //tạo đường line để kiểm thử khoảng cách trước sau khi dash
    LineRenderer lineRenderer1;
    private float lineLength;
    private Vector3 initialPosition;

    private void Start()
    {
        moveScript = GetComponent<Movement>();

        //kiểm thử
        lineLength = dashSpeed * dashTime;
        lineRenderer1 = GetComponent<LineRenderer>();
        lineRenderer1.enabled = true;
        UpdateLine();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dashes());
        }

        //kiểm thử
        UpdateLine();
    }
    IEnumerator Dashes()
    {
        canDash = false;

        //kiểm thử
        initialPosition = transform.position;
        lineRenderer1.enabled = true;
        UpdateLine();
        //

        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            UpdateLine_isdashing(); //kiểm thử
            yield return null;
        }
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    //kiểm thử
    void UpdateLine() //định vị trị trí line trước khi dash
    {
        Vector3 lineEndPosition = transform.position + transform.forward * lineLength;
        lineRenderer1.SetPosition(0, transform.position);
        lineRenderer1.SetPosition(1, lineEndPosition);

    }
    void UpdateLine_isdashing() //định vị trị trí line khi đang dash
    {
        Vector3 lineEndPosition = initialPosition + transform.forward * lineLength;
        lineRenderer1.SetPosition(0, initialPosition);
        lineRenderer1.SetPosition(1, lineEndPosition);
    }
}
