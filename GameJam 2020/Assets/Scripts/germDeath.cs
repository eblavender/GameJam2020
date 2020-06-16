using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germDeath : MonoBehaviour
{
    public float germHealth = 30f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            germHealth -= 10f;

            if (germHealth <= 0f)
            {
                GameManager.Instance.RemoveGerm(gameObject);
                Destroy(gameObject);
            }

        }
    }
}
        

