using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germDeath : MonoBehaviour
{
    public float germHealth = 30f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("hit");
            germHealth -= 10f;
            if(germHealth <= 0f)
            {
                Destroy(gameObject);
            }

        }
    }
}
        

