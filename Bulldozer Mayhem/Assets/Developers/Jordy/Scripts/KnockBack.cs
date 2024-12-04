using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 500.0f;
    public Rigidbody rb;
    public Rigidbody OpponentRb;

    public MonoBehaviour MovementScriptPlayer;
    public float DisableTime = 0.8f;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Front")
        {
            rb.AddForce(-transform.forward * force * 20, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

            Debug.Log("Front hit");
        }


        if (other.gameObject.tag == "Back")
        {
            OpponentRb.AddForce(transform.forward * force * 45, ForceMode.Impulse);
            rb.AddForce(-transform.forward * force * 15, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

            Debug.Log("Back hit");
        }

        if (other.gameObject.tag == "Left")
        {
            OpponentRb.AddForce(transform.forward * force * 20, ForceMode.Impulse);
            rb.AddForce(-transform.forward * force * 15, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

            Debug.Log("Left hit");
        }

        if (other.gameObject.tag == "Right")
        {
            OpponentRb.AddForce(transform.forward * force * 20, ForceMode.Impulse);
            rb.AddForce(-transform.forward * force * 15, ForceMode.Impulse);

            StartCoroutine(DisableMovement());

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
