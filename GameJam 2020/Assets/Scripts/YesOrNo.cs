using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum YNButton { yes, no }

public class YesOrNo : MonoBehaviour
{
    private GameSettings gameSettings;
    public Button yesButton, noButton;
    public YNButton ynb;
    

    // Start is called before the first frame update
    void Start()
    {
        /*  noButton.enabled = true;
          no = true;
          yesButton.enabled = false;
          yes = false; */

        gameSettings = GameSettings.Instance;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void YesOrNoX()
    {
        switch (ynb)
        {
            case YNButton.yes:
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(true);
                gameSettings.xAxisInvert = false;
                break;
            case YNButton.no:
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(false);
                gameSettings.xAxisInvert = true;
                break;
        }
    }
    public void YesOrNoY()
    {
        switch (ynb)
        {
            case YNButton.yes:
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(true);
                gameSettings.yAxisInvert = false;
                break;
            case YNButton.no:
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(false);
                gameSettings.yAxisInvert = true;
                break;
        }
    }
    public void YesOrNoAuto()
    {
        switch (ynb)
        {
            case YNButton.yes:
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(true);
                gameSettings.auto = false;
                break;
            case YNButton.no:
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(false);
                gameSettings.auto = true;
                break;
        }
    }

    /*  public void YesPressed()
      {
          yesButton.gameObject.SetActive(false);
          yes = false;
          noButton.gameObject.SetActive(true);
          no = true;
      }

      public void NoPressed()
      {
          yesButton.gameObject.SetActive(true);
          yes = true;
          noButton.gameObject.SetActive(false);
          no = false;
      }

      */
}
