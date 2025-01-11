using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;    // 使用 TMP_Text
    public int countdownTime = 3;     // 倒數秒數
    public string sceneName = "SampleScene"; // 要跳轉的場景名稱
    public RectTransform countdownRectTransform; // RectTransform 用於控制位置

    private Vector2 currentPosition;  // 記錄初始位置
    private float originalFontSize;   // 原始字體大小
    private AudioSource audioSource;  // 音效來源

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("CountdownText 尚未綁定！");
            return;
        }

        if (countdownRectTransform == null)
        {
            Debug.LogError("CountdownRectTransform 尚未綁定！");
            return;
        }

        // 初始化位置和字體大小
        currentPosition = countdownRectTransform.anchoredPosition;
        originalFontSize = countdownText.fontSize;

        // 獲取 AudioSource 組件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource 尚未綁定！");
            return;
        }

        // 開始倒數協程
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(2f); // 顯示 2 秒

        

        // 開始倒數
        while (countdownTime > 0)
        {
            if (countdownTime == 3)
            {
                // 播放音效
                audioSource.Play();
                // 如果倒數到 3，移動文字位置並放大
                //countdownRectTransform.anchoredPosition = new Vector2(currentPosition.x + 400, currentPosition.y + 50);
                countdownText.fontSize = originalFontSize * 1.3f;
            }

            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f); // 每秒更新一次
            countdownTime--;
        }

        // 倒數結束，顯示 "Go!"
        countdownText.text = "Go!";
        //countdownRectTransform.anchoredPosition = new Vector2(currentPosition.x + 300, currentPosition.y + 50);
        countdownText.fontSize = originalFontSize * 1.2f;

        yield return new WaitForSeconds(0.3f); // 顯示 "Go!" 的時間

        // 等待音效播放完成
        //yield return new WaitWhile(() => audioSource.isPlaying);
        //yield return new WaitForSeconds(0.5f); // 顯示 2 秒


        // 切換場景
        SceneManager.LoadScene(sceneName);
    }
}