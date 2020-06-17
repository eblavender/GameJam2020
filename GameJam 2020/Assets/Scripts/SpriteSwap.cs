using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteSwap : MonoBehaviour
{
    [SerializeField] private List<Sprite> allSprites, staticSprites;
    private Image image;

    [SerializeField] private float timedifference = 0.1f;

    private bool staticOn;
    private int spriteNo;
    private float timer = 0;
    private float staticTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        spriteNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            staticTimer -= Time.deltaTime;
            return;
        }

        if(staticTimer < 0)
        {
            staticOn = true;
            staticTimer = 5f;
            spriteNo = 0;
        }

        if (staticOn)
        {
            Static();
            return;
        }

        timer = timedifference;

        image.sprite = allSprites[spriteNo];

        if (spriteNo < allSprites.Count - 1)
            spriteNo++;
        else
            spriteNo = 0;
    }

    private void Static()
    {
        timer = timedifference;

        image.sprite = staticSprites[spriteNo];

        if (spriteNo < staticSprites.Count - 1)
            spriteNo++;
        else
        {
            staticOn = false;
            spriteNo = 0;
        }
    }
}
