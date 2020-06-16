using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GermType { Static, Timid, Hostile }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerMotor motor;
    public Slider virusSlider;

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
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = multiplyFrequency;
        MultiplyAllGerms();
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
            //Time.timeScale = 0f;
            //Game over
            //manager.
        }
    }


    private void UpdateVirusSlider()
    {
        virusSlider.value = allGerms.Count;
    }
}
