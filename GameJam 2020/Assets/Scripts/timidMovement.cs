using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timidMovement : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    public float rotateX, rotateY, rotateZ;
    public Transform Player;
    public float dist = 10f;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] private Material timidMat;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.motor.transform;

        rotateX = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateY = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateZ = Random.Range(-10f, 10f) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) <= dist)
        {
            transform.LookAt(Player);
            transform.position -= transform.forward * movementSpeed * Time.deltaTime;

            if (!timidMat.IsKeywordEnabled("_EMISSION"))
                timidMat.EnableKeyword("_EMISSION");
        }
        else
        {
            speedX = Random.Range(-1f, 1f) * Time.deltaTime;
            speedY = Random.Range(-1f, 1f) * Time.deltaTime;
            speedZ = Random.Range(-1f, 1f) * Time.deltaTime;
            transform.position += new Vector3(speedX, speedY, speedZ);
            transform.eulerAngles += new Vector3(rotateX, rotateY, rotateZ);

            if (timidMat.IsKeywordEnabled("_EMISSION"))
                timidMat.DisableKeyword("_EMISSION");
        }
    }
}
