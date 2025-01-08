using UnityEngine;
using UnityEngine.UI;

public class Coin_display : MonoBehaviour
{
    public Text coinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText.text = "Coin:0";
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin:"+CoinManager.currentGoldCoins.ToString();
    }
}
