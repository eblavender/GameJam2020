﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menuScript : MonoBehaviour
{
    public Button playButton, optionsButton, quitButton;
    public GameObject optionsMenu, playMenu, creditsMenu;
    private GameSettings gameSettings;
    public Slider mouseSens;


    void Start()
    {
        gameSettings = GameSettings.Instance;
        gameSettings.xAxisInvert = false;
        gameSettings.yAxisInvert = false;
        gameSettings.auto = false;
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        gameSettings.sensValue = mouseSens.value;
    }

    public void LoadLevel()
    {
      /*  playButton.interactable = false;
        optionsButton.interactable = false;
        quitButton.interactable = false; */
        StartCoroutine("LoadLevelButtonDelay");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    IEnumerator LoadLevelButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    public void uninteractable()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        quitButton.interactable = false;
    }

    public void OptionsPressed()
    {
        // optionsMenu.SetActive(true);
        // playMenu.SetActive(false);
        StartCoroutine("OptionsButtonDelay");
    }

    public void BackPressed()
    {
        // optionsMenu.SetActive(false);
        // playMenu.SetActive(true);
        StartCoroutine("BackButtonDelay");
    }

    public void CreditsPlayed()
    {
        StartCoroutine("CreditsButtonDelay");
    }

    IEnumerator OptionsButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        optionsMenu.SetActive(true);
        playMenu.SetActive(false);

    }

    IEnumerator BackButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        playMenu.SetActive(true);

    }

    IEnumerator CreditsButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);
        creditsMenu.SetActive(true);
        playMenu.SetActive(false);
        
    }



}
