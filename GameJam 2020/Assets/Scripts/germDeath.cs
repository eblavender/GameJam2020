using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class germDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    private AudioSource source;

    private float germHealth = 10f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            germHealth -= 10f;

            if (germHealth <= 0f)
                DestroyGerm();
        }
        else if (other.transform.CompareTag("Player"))
            DestroyGerm();
    }

    private void DestroyGerm()
    {
        GameManager.Instance.RemoveGerm(gameObject);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        source.Play();
        deathParticles.Play();

        Invoke("Kill", deathParticles.main.duration);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
        

