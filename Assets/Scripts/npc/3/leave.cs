using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics; 

public class leave: MonoBehaviour
{
    public GameObject door;
    void Start()
    {
        door.SetActive(true);
        CoinManager.currentGoldCoins += 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("door"))
        {
            //StartCoroutine(PlayAnimationAndWait());
            SceneManager.LoadScene("SampleScene");  
        }
    }
    
}
