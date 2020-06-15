using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMotor motor;



    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        motor.isFlying = true;
    }
}
