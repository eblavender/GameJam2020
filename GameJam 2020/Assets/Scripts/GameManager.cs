using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GermType { Static, Timid, Hostile }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerMotor motor;
    public Slider virusSlider;
    public GameObject victoryScreen, germDefeatScreen, pauseScreen;
    public bool pause = false, gameOver;

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

    //Cache
    private List<germMultiply> tempGerms = new List<germMultiply>();
    private float timer = 0;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
            }

            return;
        }

        //virusSlider.value = allGerms.Count;
        timer = multiplyFrequency;
        MultiplyAllGerms();

        //Debug.Log(allGerms.Count);

         
    }   

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
        allGerms.Remove(germ.GetComponent<germMultiply>());
        UpdateVirusSlider();
    }

    private void MultiplyAllGerms()
    {
        foreach (germMultiply germ in allGerms)
            tempGerms.Add(germ.MultiplyGerm().GetComponent<germMultiply>());

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
