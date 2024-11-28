using UnityEngine;

public class MovementPlayerOne : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed;
    private bool hasGrip;
    // Start is called before the first frame update
    void Start()
    {
    }


    void Update()
    {
        if (hasGrip)
        {
            //Move forward
            if (Input.GetKey(KeyCode.W))
            {
                //   transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
                transform.Translate(0, 0, 1 * Time.deltaTime * movementSpeed);

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
            if (Input.GetKey(KeyCode.S))
            {

                transform.Translate(0, 0, -1 * Time.deltaTime * movementSpeed);

            }
        }



        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2))
        {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 2, Color.yellow, 2);
                hasGrip = true; // Set hasGrip to true when the ray hits
        }

        // Note: Do not reset `hasGrip` to `false` outside this condition.

    }
}

