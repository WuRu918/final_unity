using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class To_result : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("到達旗子！切換場景中...");
            // 玩家碰到旗幟後切換到下一個場景
            SceneManager.LoadScene("result");  
        }
    }
}
