using System;
using UnityEngine;

public enum MoveState { Idle, Forward, Backward, Left, Right}
public class PlayerMotor : MonoBehaviour
{
    public Animator boostEffectAnim;
    private Rigidbody playerRigid;

    [Header("Settings")]
    public bool joystick = true;
    public bool isFlying = false;

    [Space]

    [Header("Parameters")]
    public float maxSpeed = 15;
    public float lookSpeed = 1000;
    public float thrust = 10f;

    //Cache
    private Vector3 mouseRotation;
    private float pitch, yaw;
    private float boostAmount;
    private GameSettings gameSettings;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        gameSettings = GameSettings.Instance;
    }
    void FixedUpdate()
    {
        if (!isFlying)
            return;

        CalculateMovement();
        CalculateRotation();
    }
    private void Update()
    {
        //CalculateRotation();
    }

    private void CalculateRotation()
    {
        if (gameSettings)
        {
            if (gameSettings.yAxisInvert == false)
                pitch -= Input.GetAxis("Mouse Y");
            else
                pitch += Input.GetAxis("Mouse Y");

            if (gameSettings.xAxisInvert == false)
                yaw += Input.GetAxis("Mouse X");
            else
                yaw -= Input.GetAxis("Mouse X");
        }
        else
        {
            pitch -= Input.GetAxis("Mouse Y");
            yaw += Input.GetAxis("Mouse X");
        }

        mouseRotation = new Vector3(pitch, yaw, 0) * Time.deltaTime * lookSpeed;

        transform.eulerAngles = mouseRotation;

        //playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(mouseRotation));
    }
    private void CalculateMovement()
    {
        //Verticle
        if (Input.GetKey(KeyCode.W))
            playerRigid.AddForce(transform.forward * thrust);
        else if (Input.GetKey(KeyCode.S))
            playerRigid.AddForce(-transform.forward * thrust);

        //Horizontal
        if (Input.GetKey(KeyCode.D))
            playerRigid.AddForce(transform.right * thrust);
        else if (Input.GetKey(KeyCode.A))
            playerRigid.AddForce(-transform.right * thrust);

        boostAmount = -1f + ((playerRigid.velocity.magnitude / 30f) * 2);
        boostEffectAnim.SetFloat("Boost", boostAmount);
    }
}
