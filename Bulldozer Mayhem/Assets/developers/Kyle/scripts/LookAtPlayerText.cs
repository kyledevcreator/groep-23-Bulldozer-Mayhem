using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerText : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 offset = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make the object face the camera
        Vector3 direction = cameraTransform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Apply the 180-degree rotation
        transform.rotation = lookRotation * Quaternion.Euler(offset);
    }
}
