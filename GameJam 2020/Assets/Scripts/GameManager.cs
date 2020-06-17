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

        victoryScreen.SetActive(false);
        germDefeatScreen.SetActive(false);
        pauseScreen.SetActive(false);

        pause = false;
        gameOver = false;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            virusSlider.value = allGerms.Count;
            if (allGerms.Count <= 0f)
            {
                Debug.Log("Fin");
                victoryScreen.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
            }

            if(pause == true)
            {
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            /*else
            {

                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }*/
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
        StartCoroutine("RetryButtonDelay");
        //Application.LoadLevel(Application.loadedLevel);
    }

    public void ReturnToMaiunMenu()
    {
        StartCoroutine("ReturnButtonDelay");
    }

    IEnumerator RetryButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReturnButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
