using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germMultiply : MonoBehaviour
{
    private GameManager manager;

    private Vector3 offset; 

    public GermType germType;
    private int germChance;

    void Start()
    {
        manager = GameManager.Instance;
    }

    public GameObject MultiplyGerm()
    {
        GameObject germ;

        germChance = Random.Range(0, 100);

        if (manager.hostileChance >= germChance)
            germ = Instantiate(manager.hostilePrefab, transform.position + offset, Random.rotation);
        else if (manager.timidChance >= germChance)
            germ = Instantiate(manager.timidPrefab, transform.position + offset, Random.rotation);
        else
            germ = Instantiate(manager.staticPrefab, transform.position + offset, Random.rotation);

        return germ;
    }
}
