using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    public Button playButton, optionsButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void uninteractable()
    {
        playButton.interactable = false;
        optionsButton.interactable = false;
        quitButton.interactable = false;
    }

    
}
