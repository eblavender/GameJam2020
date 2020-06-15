using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;

    private Quaternion currentRotation;
    private float wantedRotationAngleSide, currentRotationAngleSide, wantedRotationAngleUp, currentRotationAngleUp;
    private float distance = 5.0f;
    private float height = 4.0f;
    private float rotationDamping = 3.0f;

    void LateUpdate()
    {
        if (!lookAt)
            return;

        wantedRotationAngleSide = lookAt.eulerAngles.y;
        currentRotationAngleSide = transform.eulerAngles.y;

        wantedRotationAngleUp = lookAt.eulerAngles.x;
        currentRotationAngleUp = transform.eulerAngles.x;

        currentRotationAngleSide = Mathf.LerpAngle(currentRotationAngleSide, wantedRotationAngleSide, rotationDamping * Time.deltaTime);
        currentRotationAngleUp = Mathf.LerpAngle(currentRotationAngleUp, wantedRotationAngleUp, rotationDamping * Time.deltaTime);

        currentRotation = Quaternion.Euler(currentRotationAngleUp, currentRotationAngleSide, 0);

        transform.position = lookAt.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        transform.LookAt(lookAt);

        transform.position += transform.up * height;
    }
}
