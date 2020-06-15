using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int playerDamage = 10;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> availableBullets;
    private GameObject nextBullet;

    private void Start()
    {
        availableBullets = new List<GameObject>();

        for (int i = 0; i < 10; i++)
            SpawnBullet();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        nextBullet = GetFromPool();
        nextBullet.transform.position = bulletSpawn.position;
        nextBullet.transform.rotation = transform.rotation;
        nextBullet.SetActive(true);
    }

    private GameObject SpawnBullet()
    {
        GameObject go = Instantiate(bulletPrefab, transform.position, transform.rotation);
        go.GetComponent<Bullet>().combat = this;
        availableBullets.Add(go);
        return go;
    }
    private GameObject GetFromPool()
    {
        GameObject toReturn;

        if (availableBullets.Count > 0)
        {
            toReturn = availableBullets[0];
            availableBullets.RemoveAt(0);
            return toReturn;
        }
        else
            return SpawnBullet();
    }
    public void PushToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        availableBullets.Add(bullet);
    }
}
