using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;

    private float germHealth = 10f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            germHealth -= 10f;

            if (germHealth <= 0f)
            {
                GameManager.Instance.RemoveGerm(gameObject);
                GetComponentInChildren<MeshRenderer>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;

                deathParticles.Play();

                Invoke("Kill", deathParticles.main.duration);
            }

        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
        

