using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontKnockback : MonoBehaviour
{
    [SerializeField] private float force = 100.0f;
    public Rigidbody rb;
    public GameObject Player;
    public Vector3 PushDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PushDirection = Player.GetComponent<Vector3>();

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up * force, ForceMode.Impulse);
        }
    }
    
}
