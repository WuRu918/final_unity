using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for Image component
using TMPro; // for TMP_Text

public class gamecontroller: MonoBehaviour
{
    public GameObject[] questionObjects; // 存放所有題目的 GameObject（每個題目對應一個 GameObject）
    public int currentQuestionIndex = 0; // 目前題目的索引
    public int score = 0; // 分数
    public GameObject[] balloons; // 拖入所有氣球的 prefab

    public TMP_Text countdownText; // 倒数文字
    private Vector3 countdownPosition;
    public int countdownTime = 3; // 倒数秒数

    void Start()
    {
        countdownPosition = countdownText.transform.position;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            // 如果題目索引超出範圍，表示遊戲結束
            if (currentQuestionIndex >= questionObjects.Length)
            {
                countdownText.text = "Game Over!";
                yield return new WaitForSeconds(3f);
                Debug.Log("遊戲結束，最終分數: " + score);
                break; // 結束遊戲迴圈
            }

            // 重置氣球
            ResetBalloons();

            // 倒數計時
            yield return StartCoroutine(StartCountdown());

            // 等待玩家射擊時間
            yield return new WaitForSeconds(8f);

            // 檢查是否射中
            CheckResult();
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownText.fontSize = 380;
        countdownText.transform.position = new Vector3(countdownPosition.x-200f,countdownPosition.y,countdownPosition.z);
        countdownText.text = "Ready!";
        yield return new WaitForSeconds(1f);

        
        float x = this.transform.position.x;
        // 倒數計時
        while (countdownTime > 0)
        {
            countdownText.transform.position = new Vector3(countdownPosition.x,countdownPosition.y,countdownPosition.z);         
            countdownText.fontSize = 500;
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.transform.position = new Vector3(countdownPosition.x-200f,countdownPosition.y,countdownPosition.z);
        countdownText.fontSize = 380;
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // 清空倒數文字
        countdownText.text = "";

        // **按順序顯示題目圖片**
        UpdateQuestionObject();
    
        // 啟動氣球下落
        EnableBalloonMovement();
     
    }

    void UpdateQuestionObject()
    {
        // 確保上一題的 GameObject 隱藏
        if (currentQuestionIndex > 0 && currentQuestionIndex - 1 < questionObjects.Length)
        {
            questionObjects[currentQuestionIndex - 1].SetActive(false);
        }

        // 顯示當前題目的 GameObject
        questionObjects[currentQuestionIndex].SetActive(true);

        // 更新索引
        currentQuestionIndex++;
    }
    void ResetBalloons()
    {
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue; // 確保氣球實例存在
            var resettable = balloon.GetComponent<IResettable>();
            if (resettable != null)
            {
                resettable.ResetBalloon(); // 重置氣球位置和狀態
            }
        }
    }

    void EnableBalloonMovement()
    {
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue;
            var resettable = balloon.GetComponent<IResettable>();
            if (resettable != null)
            {
                ((MonoBehaviour)resettable).GetComponent<black>().canPlay = true; // 設置下落啟用
                print("Balloon: " + balloon.name + " canPlay value: " + ((MonoBehaviour)resettable).GetComponent<black>().canPlay); // 打印氣球的canPlay值
            }
        }
    }

    void CheckResult()
    {
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue;

            var resettable = balloon.GetComponent<IResettable>();
            if (resettable != null && resettable.GetHP() <= 0) // 如果氣球被擊中
            {
                if (IsCorrectAnswer(balloon))
                {
                    score += 1;
                }
            }
        }

        Debug.Log("Current Score: " + score);
    }

    bool IsCorrectAnswer(GameObject balloon)
    {
        // 獲取當前題目的 GameObject 的 tag
        string questionTag = questionObjects[currentQuestionIndex - 1].tag;
        // 得到氣球標籤
        string balloonTag = balloon.tag;
        // 比較題目tag跟被射掉的氣球tag
        return questionTag == balloonTag;
    }
}
