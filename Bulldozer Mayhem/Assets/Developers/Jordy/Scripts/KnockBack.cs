using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 100.0f;
    public Rigidbody rb;
    public GameObject Player;
    public GameObject Opponent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(-transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up * force, ForceMode.Impulse);
        }
    }
    
}
