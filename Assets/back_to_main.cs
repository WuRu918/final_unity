using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToStartScene()
    {
        SceneManager.LoadScene("start_2"); 
        CoinManager.currentGoldCoins = 0;
    }

    
}
