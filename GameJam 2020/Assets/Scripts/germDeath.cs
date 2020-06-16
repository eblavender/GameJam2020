using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germDeath : MonoBehaviour
{
    public float germHealth = 30f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("hit");
            germHealth -= 10f;
            if(germHealth <= 0f)
            {
                GameManager.Instance.allGerms.Remove(GetComponent<germMultiply>());
                Destroy(gameObject);
            }

        }
    }
}
        

