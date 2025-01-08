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
            yield return new WaitForSeconds(12f);

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
        // 遍歷所有氣球
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue;

            // 嘗試分別獲取每個氣球上掛載的腳本
            var ballloonScript = balloon.GetComponent<ballloon>();
            var pinkScript = balloon.GetComponent<pink>();
            var blackScript = balloon.GetComponent<black>();

            // 如果有對應的腳本，設置 canPlay 為 true
            if (ballloonScript != null)
            {
                ballloonScript.canPlay = true;
                print("Balloon with ballloon script canPlay: " + ballloonScript.canPlay);
            }

            if (pinkScript != null)
            {
                pinkScript.canPlay = true;
                print("Balloon with pink script canPlay: " + pinkScript.canPlay);
            }

            if (blackScript != null)
            {
                blackScript.canPlay = true;
                print("Balloon with black script canPlay: " + blackScript.canPlay);
            }

            // 嘗試獲取 Rigidbody2D 組件並設置為 Dynamic
            var rb = balloon.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                print("Rigidbody2D is missing on: " + balloon.name);
            }

            // 恢復氣球顯示
            Renderer renderer = balloon.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;  // 顯示物體
            }

            // 恢復物理和碰撞
            Rigidbody2D balloonRb = balloon.GetComponent<Rigidbody2D>();
            if (balloonRb != null)
            {
                balloonRb.simulated = true; // 恢復物理運算
            }

            Collider2D collider = balloon.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;  // 恢復碰撞檢測
            }
        }
    }

    void CheckResult()
{
    foreach (var balloon in balloons)
    {
        if (balloon == null)
        {
            Debug.LogError("Balloon is null");
            continue;
        }

        var resettable = balloon.GetComponent<IResettable>();
        if (resettable == null)
        {
            Debug.LogError("IResettable component is missing on: " + balloon.name);
            continue;
        }

        if (resettable.GetHP() <= 0) // 如果氣球被擊中並且血量小於 0
        {
            if (IsCorrectAnswer(balloon))
            {
                score += 1;
                Debug.Log("Score increased. Current Score: " + score);
            }
        }
    }
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
