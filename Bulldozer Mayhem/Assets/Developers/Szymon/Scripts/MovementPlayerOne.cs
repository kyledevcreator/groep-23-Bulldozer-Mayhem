using UnityEngine;

public class MovementPlayerOne : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed;
    private bool hasGrip;
    private Rigidbody rb;
    [SerializeField] private PlayerStatistic p1, p2;
    private PlayerStatistic activePlayer;
    public PlayerEnum player;
    public LayerMask platform;
    private int onRoofFrames;

    [SerializeField] private float roofFixTorque, roofFixForce;

    public enum PlayerEnum
    {
        Player1,
        Player2,
    }

    private void Start()
    {
        if (player == PlayerEnum.Player1)
            activePlayer = p1;
        else
            activePlayer = p2;
        rb = GetComponent<Rigidbody>();
    }


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
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 3, platform))
        {
            onRoofFrames++;
        }

        if (onRoofFrames > 60)
        {
            rb.AddForce(Vector3.up * roofFixForce, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * roofFixTorque, ForceMode.Impulse);
            onRoofFrames = 0;
        }
        if (hasGrip && player == PlayerEnum.Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {

                //transform.Translate(0, 0, 1 * Time.deltaTime * movementSpeed);
                rb.AddForce(transform.forward * (movementSpeed + activePlayer.gasSpeedBonus));

            }

            //Move right
            if (Input.GetKey(KeyCode.D))
            {

                //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                rb.AddTorque(Vector3.up * (rotationSpeed + activePlayer.torqueSpeedBonus), ForceMode.Force);

            }

            //Move left
            if (Input.GetKey(KeyCode.A))
            {

                //transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                rb.AddTorque(-Vector3.up * (rotationSpeed + activePlayer.torqueSpeedBonus), ForceMode.Force);

            }

            //Move back
            if (Input.GetKey(KeyCode.S))
            {

                //transform.Translate(0, 0, -1 * Time.deltaTime * movementSpeed);
                rb.AddForce(-transform.forward * (movementSpeed + activePlayer.reverseSpeedBonus));

            }
        }
        else if (hasGrip && player == PlayerEnum.Player2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                //transform.Translate(0, 0, 1 * Time.deltaTime * movementSpeed);
                rb.AddForce(transform.forward * movementSpeed);

            }

            //Move right
            if (Input.GetKey(KeyCode.RightArrow))
            {

                //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                rb.AddTorque(Vector3.up * rotationSpeed, ForceMode.Force);

            }

            //Move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {

                //transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                rb.AddTorque(-Vector3.up * rotationSpeed, ForceMode.Force);

            }

            //Move back
            if (Input.GetKey(KeyCode.DownArrow))
            {

                //transform.Translate(0, 0, -1 * Time.deltaTime * movementSpeed);
                rb.AddForce(-transform.forward * movementSpeed);

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

