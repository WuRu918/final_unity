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
            Debug.Log("��F�X�l�I����������...");
            // ���a�I��X�m�������U�@�ӳ���
            SceneManager.LoadScene("result");  
        }
    }
}
