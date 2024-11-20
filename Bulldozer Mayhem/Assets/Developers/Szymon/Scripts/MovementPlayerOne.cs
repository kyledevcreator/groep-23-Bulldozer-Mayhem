using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovementPlayerOne : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    
    void Update()
    {
        //Move forward
        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(transform.forward * Time.deltaTime);
            
            
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            

        }

        //Move left
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            
        }

        //Move back
        if(Input.GetKey(KeyCode.S))
        {

            transform.Translate(-transform.forward * Time.deltaTime);
            
        }

    }
}
