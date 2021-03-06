﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GermType { Static, Timid, Hostile }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerMotor motor;
    public TextMeshProUGUI virusCountText;
    public Slider virusSlider;
    public GameObject victoryScreen, germDefeatScreen, pauseScreen;
    public bool pause = false, gameOver, victory;

    [Header("Germ Settings")]
    public GameObject staticPrefab;
    public GameObject timidPrefab;
    public GameObject hostilePrefab;
    public List<germMultiply> allGerms;
    [HideInInspector] public List<germDeath> germsDying;
    [SerializeField] private float multiplyFrequency = 5f;
    [SerializeField] private int maxGerms = 200;
    [Range(0, 100)]
    public int staticChance, timidChance, hostileChance;
    private int deathCount;

    private germMultiply tempMultiply;

    //Cache
    private List<germMultiply> tempGerms = new List<germMultiply>();
    private float timer = 0;

    #region Unity Functions

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        timer = multiplyFrequency;

        virusSlider.maxValue = maxGerms;
        virusSlider.value = allGerms.Count;

        if(victoryScreen)
            victoryScreen.SetActive(false);
        if (germDefeatScreen)
            germDefeatScreen.SetActive(false);
        if (pauseScreen)
            pauseScreen.SetActive(false);

        pause = false;
        gameOver = false;
        victory = false;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gameOver && !victory)
            PauseGame();
        }
        

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            virusSlider.value = allGerms.Count;
            if (allGerms.Count <= 0f)
            {
                if(!victoryScreen.activeInHierarchy)
                    victoryScreen.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                victory = true;
            }

            return;
        }

        //virusSlider.value = allGerms.Count;
        timer = multiplyFrequency;
        MultiplyAllGerms();

        //Debug.Log(allGerms.Count);

         
    }

    #endregion

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        motor.isFlying = true;
    }
    private void PauseGame()
    {
        pause = !pause;

        pauseScreen.SetActive(pause);

        if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void RemoveGerm(GameObject germ)
    {
        deathCount++;
        virusCountText.text = deathCount.ToString("n0");

        allGerms.Remove(germ.GetComponent<germMultiply>());
        UpdateVirusSlider();
    }

    private void MultiplyAllGerms()
    {
        foreach (germMultiply germ in allGerms)
        {
            if (germ.hasMultiplied && allGerms.Count > 25)
                continue;

            tempMultiply = germ.MultiplyGerm().GetComponent<germMultiply>();
            tempGerms.Add(tempMultiply);
        }

        foreach (germMultiply germ in tempGerms)
            allGerms.Add(germ);

        tempGerms.Clear();

        if (allGerms.Count >= maxGerms)
        {                                                                                            //if max germs > 100 then game over
            germDefeatScreen.SetActive(true);
            Debug.Log("Game over");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOver = true;
            //manager.
        }

        GetComponent<AudioSource>().Play();
    }


    private void UpdateVirusSlider()
    {
        virusSlider.value = allGerms.Count;

    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMaiunMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetTimeInAction()
    {
        Time.timeScale = 1f;
    }
}
