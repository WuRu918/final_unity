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
            award = "�A�����i�H�Y�j�\~";
        }
        else
        {
            award = "�A�S���Y���FQQ";
        }
        result_text.text = "�A����" + CoinManager.currentGoldCoins.ToString() + "�Ӫ���"+Environment.NewLine+award;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
