using System;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    public Text result_text;
    public string award="";
    public GameObject Object;
    public Image result_image;
    public Sprite no_time;
    public Sprite great_meal;
    public Sprite poor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (!timecontrol.enough_time)
        {
            result_image.sprite = no_time;
            result_text.text = "No time for lunch...time to go to class.";
        }
        else
        {
            if (CoinManager.currentGoldCoins > 20)
            {
                result_image.sprite = great_meal;
                award = "You can have a great meal";
            }
            else
            {
                result_image.sprite = poor;
                award = "You don't have enough money...";
            }
            result_text.text = "you have " + CoinManager.currentGoldCoins.ToString() + " coins" + Environment.NewLine + award;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
