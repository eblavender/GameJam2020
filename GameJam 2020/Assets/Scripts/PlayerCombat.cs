using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private const int MAX_SHIELD = 100;
    private const int RECHARGE_COOLDOWN = 3;

    [Header("Health and Shields")]
    public int playerDamage = 10;
    public int health = 100;
    public int shield = 100;
    public float rechargeSpeed = 1;

    public bool hasShield = true;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shieldObject;

    private List<GameObject> availableBullets;
    private GameObject nextBullet;

    private float timer = 3f;

    private void Start()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = health;
        healthSlider.value = health;

        shieldSlider.minValue = 0;
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;

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

        if (timer > 0)
            timer -= Time.deltaTime;
        else if (shield != MAX_SHIELD)
            shield += Mathf.RoundToInt(Time.deltaTime * rechargeSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //Take damage from virus
            TakeDamage(10);
        }
    }

    public void FireBullet()
    {
        nextBullet = GetFromPool();
        nextBullet.transform.position = bulletSpawn.position;
        nextBullet.transform.rotation = transform.rotation;
        nextBullet.SetActive(true);
    }
    private void TakeDamage(int amount)
    {
        int difference;

        if (hasShield)
        {
            shield -= amount;

            if (shield <= 0)
            {
                RemoveShield();

                difference = Math.Abs(shield);
                health -= difference;
                shield = 0;

                CheckDeath();
            }
            else
            {
                //Still has shield left
                timer = RECHARGE_COOLDOWN;
                return;
            }
        }
        else
        {
            health -= amount;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        healthSlider.value = health;
        shieldSlider.value = shield;
    }
    private void CheckDeath()
    {
        if(health <= 0)
        {
            //Dead
            //Trigger big o'l explosion
        }
    }

    private void GiveShield()
    {
        shieldObject.SetActive(true);
        hasShield = true;
    }
    private void RemoveShield()
    {
        shieldObject.SetActive(false);
        hasShield = false;
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
