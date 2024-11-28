using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTester : MonoBehaviour
{
    public float speed = 0.03f;
    public GameObject Player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 1 * speed, 0);
    }
}
