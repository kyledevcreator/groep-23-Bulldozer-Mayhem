using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public float GroundDistance = 0.1f;
    public LayerMask GroundLayer;
    public MonoBehaviour PlayerMovement;
    public Rigidbody rb;
    private bool isGrounded;
    public float baseAngle = 90f;
    public float rotationSpeed = 1.5f;


    void Update()
    {
        Quaternion baseRotation = Quaternion.Euler(0f, 0f, 0f);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, GroundDistance, GroundLayer);

        if (isGrounded == false)
        {
            //rb.transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.deltaTime * rotationSpeed);
            PlayerMovement.enabled = false;
        }

        if (isGrounded == true)
        {
            PlayerMovement.enabled = true;
        }
    }
}
