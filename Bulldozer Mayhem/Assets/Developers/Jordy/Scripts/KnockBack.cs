using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 500.0f;
    public Rigidbody rb;
    public Rigidbody OpponentRb;

    public MonoBehaviour MovementScriptPlayer;
    public float DisableTime = 1.2f;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Front")
        {
            rb.AddForce(-rb.velocity.normalized * force * 15, ForceMode.Impulse);
            OpponentRb.AddForce(rb.velocity.normalized * force * 15, ForceMode.Impulse);
            //StartCoroutine(DisableMovement());

            Debug.Log("Front hit");
        }


        if (other.gameObject.tag == "Back")
        {
            OpponentRb.AddForce(rb.velocity.normalized * force * 90, ForceMode.Impulse);
            rb.AddForce(-rb.velocity.normalized * force * 15, ForceMode.Impulse);

            //StartCoroutine(DisableMovement());

            Debug.Log("Back hit");
        }

        if (other.gameObject.tag == "Left")
        {
            OpponentRb.AddForce(rb.velocity.normalized * force * 70, ForceMode.Impulse);
            rb.AddForce(-rb.velocity.normalized * force * 15, ForceMode.Impulse);

            //StartCoroutine(DisableMovement());

            Debug.Log("Left hit");
        }

        if (other.gameObject.tag == "Right")
        {
            OpponentRb.AddForce(rb.velocity.normalized * force * 70, ForceMode.Impulse);
            rb.AddForce(-rb.velocity.normalized * force * 15, ForceMode.Impulse);

            //StartCoroutine(DisableMovement());

            Debug.Log("Right hit");
        }
    }


    IEnumerator DisableMovement()
    {
        if (MovementScriptPlayer != null)
        {

            //Zet het Movement script uit voor een halve seconde wanneer de spelers colliden.
            MovementScriptPlayer.enabled = false;

            yield return new WaitForSeconds(DisableTime);

            //Zet het Movement script weer aan.
            MovementScriptPlayer.enabled = true;
        }
    }

}
