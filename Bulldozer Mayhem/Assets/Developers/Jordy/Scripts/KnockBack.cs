using System.Collections;
using UnityEngine;
using static MovementPlayerOne;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force;
    private float addedForce;
    public Rigidbody rb;
    public Rigidbody OpponentRb;

    public MonoBehaviour MovementScriptPlayer;
    public float DisableTime = 1.2f;
    [SerializeField] private PlayerStatistic p1, p2;
    private PlayerStatistic me, opponent;
    public PlayerEnum player;
    private float powerValue;

    private void Start()
    {
        if (player == PlayerEnum.Player1)
        {
            me = p1;
            opponent = p2;
        }
        else
        {
            me = p2;
            opponent = p1;
        }
        if (gameObject.CompareTag("Front"))
            powerValue = me.frontPower;
        else if (gameObject.CompareTag("Left"))
            powerValue = me.leftPower;
        else if (gameObject.CompareTag("Right"))
            powerValue = me.rightPower;
        else if (gameObject.CompareTag("Back"))
            powerValue = me.leftPower;
        else
            Debug.LogError(gameObject.name + "does not have a valid TAG (" + gameObject.tag + "), Please change TAG to valid value.");
    }

    private void Update()
    {
        addedForce = force + powerValue;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Front")
        {
            rb.AddForce(-rb.velocity.normalized * force * (1 / me.frontStrength), ForceMode.Impulse);
            OpponentRb.AddForce(rb.velocity.normalized * addedForce * (1 / opponent.frontStrength), ForceMode.Impulse);
        }


        if (other.gameObject.tag == "Back")
        {
            rb.AddForce(-rb.velocity.normalized * force * (1 / me.backStrength), ForceMode.Impulse);
            OpponentRb.AddForce(rb.velocity.normalized * addedForce * (3 / opponent.backStrength), ForceMode.Impulse);
        }

        if (other.gameObject.tag == "Left")
        {
            rb.AddForce(-rb.velocity.normalized * force * (1 / me.leftStrength), ForceMode.Impulse);
            OpponentRb.AddForce(rb.velocity.normalized * addedForce * (2 / opponent.leftStrength), ForceMode.Impulse);
        }

        if (other.gameObject.tag == "Right")
        {
            rb.AddForce(-rb.velocity.normalized * force * (1 / me.rightStrength), ForceMode.Impulse);
            OpponentRb.AddForce(rb.velocity.normalized * addedForce * (2 / opponent.rightStrength), ForceMode.Impulse);
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
