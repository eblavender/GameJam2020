using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Rigidbody playerRigid;

    [Header("Settings")]
    public bool joystick = true;
    public bool isFlying = false;

    [Space]

    [Header("Parameters")]
    public float moveSpeed = 18;
    public float lookSpeed = 1000;

    //Cache
    private Vector3 mouseRotation, playerMovement;
    private float pitch, yaw;
    private float mouseSensitivity = 0.3f;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!isFlying)
            return;

        CalculateMovement();
        CalculateRotation();
    }

    private void CalculateRotation()
    {
        pitch = -Input.GetAxis("Mouse Y");
        yaw = Input.GetAxis("Mouse X");

        mouseRotation = new Vector3(pitch, yaw, 0) * mouseSensitivity * Time.deltaTime * lookSpeed;

        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(mouseRotation));
    }
    private void CalculateMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Move the Rigidbody forwards constantly at speed
            playerRigid.velocity = transform.forward * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Move the Rigidbody backwards constantly at speed
            playerRigid.velocity = -transform.forward * moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the ship about the Y axis in the positive direction
            playerRigid.velocity = transform.right * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //Rotate the ship about the Y axis in the negative direction
            playerRigid.velocity = -transform.right * moveSpeed;
        }
    }
}
