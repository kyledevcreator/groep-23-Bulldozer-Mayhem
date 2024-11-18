using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGravity : MonoBehaviour
{

    public GameObject platform;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GravityDelay());
    }


    private IEnumerator GravityDelay()
    {
        yield return new WaitForSeconds(2);
        rb.useGravity = true;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

}
