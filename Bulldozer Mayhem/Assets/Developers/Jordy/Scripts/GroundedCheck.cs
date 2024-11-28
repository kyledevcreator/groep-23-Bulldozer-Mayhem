using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public GameObject Player;
    public bool isGrounded;
    public float GroundDistance;


    void Start()
    {
        isGrounded = true;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

