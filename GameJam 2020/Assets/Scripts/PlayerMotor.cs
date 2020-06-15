using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Rigidbody playerRigid;

    [Header("Settings")]
    public bool joystick = true;

    [Space]

    [Header("Parameters")]
    public float moveSpeed = 18;
    public float lookSpeed = 1000;

    //Cache
    private Vector3 mouseRotation;
    private float pitch, yaw;
    private float mouseSensitivity = 0.3f;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    void FixedUpdate()
    {
        CalculateRotation();
        CalculateMovement();
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

        if (Input.GetKey(KeyCode.S))
        {
            //Move the Rigidbody backwards constantly at speed
            playerRigid.velocity = -transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the ship about the Y axis in the positive direction
            transform.Rotate(Vector3.up * Time.deltaTime * moveSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Rotate the ship about the Y axis in the negative direction
            transform.Rotate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);
        }
    }
}
