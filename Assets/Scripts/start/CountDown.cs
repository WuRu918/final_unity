using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;    // 使用 TMP_Text

    public int countdownTime = 3;     // 倒數秒數
    public string sceneName = "SampleScene"; // 要跳轉的場景名稱

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("CountdownText 尚未綁定！");
            return;
        }

        // 開始倒數流程協程
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        // 顯示 "Ready"
        countdownText.text = "Ready";
        yield return new WaitForSeconds(2f); // 顯示 2 秒

        // 開始倒數
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f); // 每秒更新一次
            countdownTime--;
        }

        // 倒數結束，顯示 "Go!"
        countdownText.text = "Go!";
        yield return new WaitForSeconds(0.5f); // 顯示 "Go!" 的時間

        // 切換場景
        SceneManager.LoadScene(sceneName);
    }
}
