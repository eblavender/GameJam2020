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
        float randomX = Random.Range(-7f, 7f);
        float randomY = Random.Range(-7f, 7f);                            //min 6 max 6 sitance from parent germ
        float randomZ = Random.Range(-7f, 7f);

        if (randomX >= -1.5f && randomX <= 0f)
            randomX = -1.5f;
        else if (randomX <= 1.5f && randomX >= 0f)
            randomX = 1.5f;

        if (randomY >= -1.5f && randomY <= 0f)
            randomY = -1.5f;
        else if (randomY <= 1.5f && randomY >= 0f)                                       //making sure the germs dont merge with one another
            randomY = 1.5f;

        if (randomZ >= -1.5f && randomZ <= 0f)
            randomZ = -1.5f;
        else if (randomZ <= 1.5f && randomZ >= 0f)
            randomZ = 1.5f;

        offset = new Vector3(randomX, randomY, randomZ);

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
