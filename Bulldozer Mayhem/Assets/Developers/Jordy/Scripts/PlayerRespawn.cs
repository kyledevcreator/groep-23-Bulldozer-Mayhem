using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody rb2;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                other.gameObject.transform.position = Vector3.up;
                other.gameObject.transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.zero;
                rb2.velocity = Vector3.zero;
            }
    }
}
