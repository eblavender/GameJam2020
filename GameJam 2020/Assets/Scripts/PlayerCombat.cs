using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;

public class PlayerCombat : MonoBehaviour
{
    private const int MAX_SHIELD = 100;
    private const int RECHARGE_COOLDOWN = 10;

    private PlayerMotor motor;

    [Header("Health and Shields")]
    public int playerDamage = 10;
    public float health = 100;
    public float shield = 100;
    public float rechargeSpeed = 1;

    public AudioSource audioPlayer;
    public AudioClip[] randomFireSound;
    public AudioClip explosionSound;

    public GameObject playerDefeatScreen;

    public bool hasShield = true;
    [SerializeField] private bool isDead = false;


    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shieldObject;


    private List<GameObject> availableBullets;
    private GameObject nextBullet;
    [SerializeField] private ParticleSystem explosion, shooting;

    private Color originalShieldColor;

    private float timer = 3f;

    private void Start()
    {
        originalShieldColor = shieldObject.GetComponent<MeshRenderer>().material.color;
        motor = GetComponent<PlayerMotor>();

        playerDefeatScreen.SetActive(false);

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
        if (!motor.isFlying || isDead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }

        if (timer > 0)
            timer -= Time.deltaTime;
        else if (shield != MAX_SHIELD)
        {
            shield += Time.deltaTime * rechargeSpeed;
            UpdateUI();
        }

        if (shield > 0 && !shieldObject.activeInHierarchy)
            GiveShield();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead)
            return;

        if (collision.transform.CompareTag("Germ"))
        {
            //Take damage from virus
            TakeDamage(10);
        }

        if (!shieldObject.activeInHierarchy)
            return;

        Sequence shieldExpand = DOTween.Sequence();
        shieldExpand.Append(shieldObject.transform.DOScale(new Vector3(0.013f, 0.013f, 0.013f), 0.1f).SetEase(Ease.Linear))
            .Append(shieldObject.transform.DOScale(new Vector3(0.011f, 0.011f, 0.011f), 0.1f).SetEase(Ease.Linear));

        Sequence shieldColor = DOTween.Sequence();
        shieldColor.Append(shieldObject.GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.1f))
            .Append(shieldObject.GetComponent<MeshRenderer>().material.DOColor(originalShieldColor, 0.1f));
    }

    public void FireBullet()
    {
        nextBullet = GetFromPool();
        nextBullet.transform.position = bulletSpawn.position;
        nextBullet.transform.rotation = transform.rotation;
        nextBullet.SetActive(true);
        shooting.Play();
        RandomBulletSound();
    }

    public void RandomBulletSound()
    {

        audioPlayer.clip = randomFireSound[Random.Range(0,randomFireSound.Length)];
        audioPlayer.Play();

    }


    private void TakeDamage(int amount)
    {
        float difference;

        if (hasShield)
        {
            shield -= amount;

            if (shield <= 0)
            {
                RemoveShield();

                difference = Math.Abs(shield);
                health -= difference;
                shield = 0;
            }
        }
        else
        {
            health -= amount;
        }

        timer = RECHARGE_COOLDOWN;
        UpdateUI();
        CheckDeath();
    }

    private void UpdateUI()
    {
        healthSlider.value = Mathf.RoundToInt(health);
        shieldSlider.value = Mathf.RoundToInt(shield);
    }
    private void CheckDeath()
    {
        if(health <= 0)
        {
            //Dead
            //Trigger big o'l explosion
            isDead = true;
            explosion.Play();
            audioPlayer.clip = explosionSound;
            audioPlayer.Play();
            GameManager.Instance.gameOver = true;
            playerDefeatScreen.SetActive(true);
            Destroy(GetComponentInChildren<MeshRenderer>());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.2f;
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
