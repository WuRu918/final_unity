using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class To_result : MonoBehaviour
{
    private timecontrol timeControlScript;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("��F�X�l�I����������...");
            // ���a�I��X�m�������U�@�ӳ���
            SceneManager.LoadScene("result");  
            timecontrol timeControlScript = GameObject.Find("YourGameObjectName").GetComponent<timecontrol>();
            int seconds = timeControlScript.m_seconds;  // 获取 m_seconds
            int minutes = timeControlScript.m_min;      // 获取 m_min
            int secondsInMin = timeControlScript.m_sec; // 获取 m_sec

            seconds = 0;
            minutes = 3;
            secondsInMin = 0;
        }
    }
}
