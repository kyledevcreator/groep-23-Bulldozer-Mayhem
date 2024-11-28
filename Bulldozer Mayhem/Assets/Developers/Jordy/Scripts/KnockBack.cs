using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 125.0f;
    public Rigidbody rb;
    public Rigidbody OpponentRb;

    public MonoBehaviour MovementScriptP1;
    public MonoBehaviour MovementScriptP2;
    public float DisableTime = 0.5f;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Front")
        {
            rb.AddForce(-transform.forward * force, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

            Debug.Log("Front hit");
        }


        if (other.gameObject.tag == "Back")
        {
            OpponentRb.AddForce(transform.forward * force * 3, ForceMode.Impulse);
            rb.AddForce(-transform.forward * force, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

            Debug.Log("Back hit");
        }

    }

    IEnumerator DisableMovement()
    {
        if (MovementScriptP1 && MovementScriptP2 != null)
        {
            MovementScriptP1.enabled = false;
            MovementScriptP2.enabled = false;

            yield return new WaitForSeconds(DisableTime);

            MovementScriptP1.enabled = true;
            MovementScriptP2.enabled = true;
        }
    }

}
