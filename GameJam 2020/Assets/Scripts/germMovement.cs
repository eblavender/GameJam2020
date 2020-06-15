using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germMovement : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    public float rotateX, rotateY, rotateZ;
    //public string status;
   // public int germBehaviour;
   // public Transform Player;
   // public float hostileDistance = 6f;
   // public float timidDistance = 10f;

    /* void Awake()
     {

     }
     */

    void Start()
    {/*
        germBehaviour = Random.Range(0, 13);
        if(germBehaviour >= 0 && germBehaviour <= 6)
        {
            Debug.Log("Static");
            status = "Static";
        }
        else if (germBehaviour >= 7 && germBehaviour <= 9)
        {
            Debug.Log("Timid");
            status = "Timid";
        }
        else if (germBehaviour >= 10 && germBehaviour <= 12)
        {
            Debug.Log("Hostile");
            status = "Hostile";
        }
        /* speedX = Random.Range(-0.5f, 0.5f) * Time.deltaTime;
         speedY = Random.Range(-0.5f, 0.5f) * Time.deltaTime;
         speedZ = Random.Range(-0.5f, 0.5f) * Time.deltaTime; */
        rotateX = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateY = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateZ = Random.Range(-10f, 10f) * Time.deltaTime;
    }

 
    void Update()
    {
      /*  if (Vector3.Distance(transform.position, Player.position) <= hostileDistance && status == "Hostile")
        {
            transform.LookAt(Player);
            transform.position += transform.forward * 3f * Time.deltaTime;
        }
        else if (Vector3.Distance(transform.position, Player.position) <= timidDistance && status == "Timid")
        {
            transform.LookAt(Player);
            transform.position -= transform.forward * 6f * Time.deltaTime;
        } */
       // else
      //  {
            idleAround();
       // }
    }

    private void idleAround()
    {
        speedX = Random.Range(-1f, 1f) * Time.deltaTime;
        speedY = Random.Range(-1f, 1f) * Time.deltaTime;
        speedZ = Random.Range(-1f, 1f) * Time.deltaTime;
        transform.position += new Vector3(speedX, speedY, speedZ);
        transform.eulerAngles += new Vector3(rotateX, rotateY, rotateZ);
    }

  /*  void OnTriggerEnter(Collider other)
    {
        if(status == "hostile" && other.tag == "Player" && Vector3.Distance(transform.position, Player.transform.position) <= minDistance)
        {
            Debug.Log("Hit");
            transform.LookAt(Player.transform);
            transform.position += transform.forward * 3f * Time.deltaTime;
        }
    }
    */
}
