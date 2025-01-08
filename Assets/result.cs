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
            award = "你有錢可以吃大餐~";
        }
        else
        {
            award = "你沒錢吃飯了QQ";
        }
        result_text.text = "你拿到" + CoinManager.currentGoldCoins.ToString() + "個金幣"+Environment.NewLine+award;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
