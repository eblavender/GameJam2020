using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public PlayerCombat combat;

    private const int MAX_DISTANCE = 100;

    private bool isActive = false;
    [SerializeField] private float bulletSpeed;
    private float distance = 0;

    private void OnEnable()
    {
        isActive = true;
    }
    private void OnDisable()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
            distance = Vector3.Distance(transform.position, combat.transform.position);

            if(distance > MAX_DISTANCE)
                combat.PushToPool(gameObject);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //Deal damage to enemy
            //Push back to pool
            
        }

        combat.PushToPool(gameObject);
    }
}
