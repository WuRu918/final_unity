using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class count: MonoBehaviour
{
    public TMP_Text countdownText;    // 使用 TMP_Text
    public int countdownTime = 3;     // 倒數秒數
    
    void Start()
    {
        // 開始倒數協程
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(5f); // 顯示 2 秒

        // 開始倒數
        while (countdownTime > 0)
        {

            // 播放音效
            //audioSource.Play();
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f); // 每秒更新一次
            countdownTime--;
        }
        
    }
}
