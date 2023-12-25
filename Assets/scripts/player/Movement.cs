using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; //player speed

    // Update is called once per frame
    void Update()
    {
        // info from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Tinh toan huong di chuyen
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // check vector di chuyen xem co > 1 khong
        if (moveDirection.magnitude > 1f)
        {
            // neu co, chuan hoa no
            moveDirection.Normalize();
        }

        // Di chuyen player voi toc do duoc dat
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
