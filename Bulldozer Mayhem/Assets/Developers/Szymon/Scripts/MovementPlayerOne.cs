using UnityEngine;

public class MovementPlayerOne : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed;
    private bool hasGrip;

    private enum PlayerEnum
    {
        Player1,
        Player2,


    }

    [SerializeField] private PlayerEnum player;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.layer == 3)
         {
             hasGrip = true;
            
         }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            hasGrip = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            hasGrip = false;
        }
        Debug.Log("Collision ended");
    }

    void Update()
    {
        if (hasGrip && player == PlayerEnum.Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {

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
        else if (hasGrip && player == PlayerEnum.Player2)
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
       
        //Move forward




        /*
        RaycastHit hit;

        // Perform the raycast
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            hasGrip = false; // Set hasGrip to false when the ray doesn't hit
        }
        else
        {
            hasGrip = true; // Set hasGrip to true when the ray hits
        }

        // Note: Do not reset `hasGrip` to `false` outside this condition.
        */
    }
}

