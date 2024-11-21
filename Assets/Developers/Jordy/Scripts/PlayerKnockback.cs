using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float force = 1250.0f;
    public Rigidbody rb;
    public GameObject Player;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(-transform.forward * force, ForceMode.Impulse);
        }
    }
}
