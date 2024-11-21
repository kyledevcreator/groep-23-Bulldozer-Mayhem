using UnityEngine;

public class MovementPlayerTwo : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }


    void Update()
    {
        //Move forward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //   transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
            transform.Translate(0, 0, 1 * Time.deltaTime * movementSpeed);

        }

        //Move right
        if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);


        }

        //Move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);

        }

        //Move back
        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.Translate(0, 0, -1 * Time.deltaTime * movementSpeed);

        }

    }
}