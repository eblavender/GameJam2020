using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hostileMovement : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    public float rotateX, rotateY, rotateZ;
    public Transform Player;
    public float dist = 6f;
    [SerializeField] float movementSpeed = 3f;
   // [SerializeField] private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.motor.transform;

        rotateX = Random.Range(-1f, 1f);
        rotateY = Random.Range(-1f, 1f);
        rotateZ = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) <= dist)
        {
            transform.LookAt(Player);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
           
        }
        else
        {
            speedX = Random.Range(-5f, 5f) * Time.deltaTime ;
            speedY = Random.Range(-5f, 5f) * Time.deltaTime ;
            speedZ = Random.Range(-5f, 5f) * Time.deltaTime ;
            transform.position += new Vector3(speedX, speedY, speedZ);
            transform.eulerAngles += new Vector3(rotateX, rotateY, rotateZ);
        }
    }
}
