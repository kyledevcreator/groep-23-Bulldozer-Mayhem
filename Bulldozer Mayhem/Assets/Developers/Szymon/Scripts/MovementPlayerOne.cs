using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("W key was pressed");
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
