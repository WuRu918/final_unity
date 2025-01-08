using System;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    public Text result_text;
    public string award="";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CoinManager.currentGoldCoins > 20)
        {
            
            award = "You can have a great meal";
        }
        else
        {
            
            award = "You don't have enough money...";
        }
        result_text.text = "you have " + CoinManager.currentGoldCoins.ToString() + " coins"+Environment.NewLine+award;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
