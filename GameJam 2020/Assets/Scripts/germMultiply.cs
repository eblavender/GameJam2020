using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class germMultiply : MonoBehaviour
{

    public GameObject germ, staticGerm, hostileGerm, timidGerm;
    public Vector3 offset; 
    public float spawnTime = 15f;                                    
    public int maxGerms = 200;
    public int germBehaviour;
    public string status;
    private GameObject[] getCount;                     

    void Start()
    {
        StartCoroutine("GermMultiply");             //start the split thingy        
    }


    void Update()
    {
        CheckForGerms();                          //check for germs in the scene
    }

    public IEnumerator GermMultiply()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            float randomX = Random.Range(-7f, 7f);
            float randomY = Random.Range(-7f, 7f);                             //min 6 max 6 sitance from parent germ
            float randomZ = Random.Range(-7f, 7f); 

            if (randomX >= -1.5f && randomX <= 0f)
            {
                randomX = -1.5f;
            }
            else if(randomX <= 1.5f && randomX >= 0f)
            {
                randomX = 1.5f;
            }

            if (randomY >= -1.5f && randomY <= 0f)
            {
                randomY = -1.5f;
            }
            else if (randomY <= 1.5f && randomY >= 0f)                                       //making sure the germs dont merge with one another
            {
                randomY = 1.5f;
            }

            if (randomZ >= -1.5f && randomZ <= 0f)
            {
                randomZ = -1.5f;
            }
            else if (randomZ <= 1.5f && randomZ >= 0f)
            {
                randomZ = 1.5f;
            }

            offset = new Vector3(randomX, randomY, randomZ);

            germBehaviour = Random.Range(1, 13);
            if (germBehaviour >= 0 && germBehaviour <= 6)
            {
                Debug.Log("Static");
                status = "Static";
            }
            else if (germBehaviour >= 7 && germBehaviour <= 9)
            {
                Debug.Log("Timid");
                status = "Timid";
            }
            else if (germBehaviour >= 10 && germBehaviour <= 12)
            {
                Debug.Log("Hostile");
                status = "Hostile";
            }

            switch (status)
            {
                case "Static":
                    Instantiate(staticGerm, germ.transform.position + offset, Random.rotation);
                    break;
                case "Timid":
                    Instantiate(timidGerm, germ.transform.position + offset, Random.rotation);
                    break;
                case "Hostile":
                    Instantiate(hostileGerm, germ.transform.position + offset, Random.rotation);
                    break;
            }
  //          Instantiate(germ, germ.transform.position + offset, Random.rotation);                         //spawn the second germ
        }
    }

    void CheckForGerms()
    {
        getCount = GameObject.FindGameObjectsWithTag("Germ");
        if (getCount.Length >= maxGerms)
        {                                                                                            //if max germs > 100 then game over
            Time.timeScale = 0f;
        }
    }
}
