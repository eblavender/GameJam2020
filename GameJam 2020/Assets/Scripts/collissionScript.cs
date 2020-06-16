using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pushbackDir { north, south, east, west, up, down }

public class collissionScript : MonoBehaviour
{
    public float pushBack = 1000f;
    public pushbackDir psback;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        switch (psback)
        {
            case pushbackDir.north:
                rb.AddForce(new Vector3(0, 0, -pushBack));
                break;
            case pushbackDir.south:
                rb.AddForce(new Vector3(0, 0, pushBack));
                break;
            case pushbackDir.east:
                rb.AddForce(new Vector3(-pushBack, 0, 0));
                break;
            case pushbackDir.west:
                rb.AddForce(new Vector3(pushBack, 0, 0));
                break;
            case pushbackDir.up:
                rb.AddForce(new Vector3(0, -pushBack, 0));
                break;
            case pushbackDir.down:
                rb.AddForce(new Vector3(0, pushBack, 0));
                break;
            default:
                rb.AddForce(new Vector3(0, 0, 0));
                break;
        }


        // rb.AddForce(new Vector3(pushBack, 0, 0));
    }
}
