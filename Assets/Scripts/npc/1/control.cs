using UnityEngine;
using System.Collections;
using TMPro; // for TMP_Text
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    public GameObject image;
    public GameObject[] questionObjects; // 存放所有题目的 GameObject
    public GameObject[] ex;             // 額外的提示或元素
    public int currentQuestionIndex = 0; // 當前題號
    public int score = 0; // 分數
    public GameObject[] balloons; // 三顆氣球

    public TMP_Text countdownText; // 倒數計時
    private Vector3 countdownPosition;
    public int countdownTime = 3; // 秒數
    public GameObject door;
    bool next = true;
    public int pointnumber;

    void Start()
    {
        // 隱藏所有照片（GameObject）
        foreach (var question in questionObjects)
        {
            question.SetActive(false);
        }

        foreach (var balloon in balloons)
        {
            balloon.SetActive(false);
        }

        foreach (var x in ex)
        {
            x.SetActive(false);
        }

        countdownPosition = countdownText.transform.position;
        door.SetActive(false);
        image.SetActive(true);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        image.SetActive(false);

        // 倒數計時準備開始
        yield return StartCoroutine(StartCountdown());
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (currentQuestionIndex <= questionObjects.Length)
        {
            next = false; // 初始化為 false
            Debug.Log("當前題號: " + currentQuestionIndex);

            // 如果題目索引超出範圍，遊戲結束
            if (currentQuestionIndex >= questionObjects.Length)
            {
                // 隱藏所有照片（GameObject）
                foreach (var question in questionObjects)
                {
                    question.SetActive(false);
                }

                foreach (var x in ex)
                {
                    x.SetActive(false);
                }
                countdownText.fontSize = 0.5f;
                //countdownText.transform.position = new Vector3(countdownPosition.x - 300f, countdownPosition.y+100f, countdownPosition.z);
                countdownText.text = "Game Over!";
                yield return new WaitForSeconds(1.5f);
                countdownText.text = "Go Left To Leave !";
                Debug.Log("遊戲結束，最終分數: " + score);
                door.SetActive(true);
                yield break; // 結束遊戲
            }

            // 顯示題目和初始化
            ResetBalloons();
            yield return StartCoroutine(UpdateQuestionObject());

            // 等待玩家回答
            while (!next)
            {
                yield return null; // 等待直到 next 被設為 true
            }

            // 確保有短暫的時間過渡到下一題
            yield return new WaitForSeconds(1f);

            // 切換到下一題
            currentQuestionIndex++;
        }
    }

    public GameObject playerObject;
    // 碰到門就可以離開
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            SceneManager.LoadScene("SampleScene");
            GameObject g = GameObject.Find(pointnumber.ToString()) as GameObject;
            playerObject = GameObject.Find("Player (1)");
            playerObject.transform.position = g.transform.position;
        }
    }

    void ResetBalloons()
    {
        foreach (var balloon in balloons)
        {
            balloon.SetActive(true);
        }
    }

    private IEnumerator StartCountdown()
    {
        
        //countdownText.transform.position = new Vector3(countdownPosition.x - 200f, countdownPosition.y, countdownPosition.z);
        countdownText.text = "Ready!";
        yield return new WaitForSeconds(1.5f);

        // 倒計時
        while (countdownTime > 0)
        {
            //countdownText.transform.position = new Vector3(countdownPosition.x - 200f, countdownPosition.y, countdownPosition.z);
            countdownText.text = "Ready!";
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        //countdownText.transform.position = new Vector3(countdownPosition.x - 200f, countdownPosition.y, countdownPosition.z);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // 清空倒計時文字
        countdownText.text = "";
    }

    IEnumerator UpdateQuestionObject()
    {
        countdownTime = 5;
        next = false;

        // 確保當前題目顯示，其他題目隱藏
        if (currentQuestionIndex >= 0 && currentQuestionIndex < questionObjects.Length)
        {
            foreach (var question in questionObjects)
            {
                question.SetActive(false);
            }
            questionObjects[currentQuestionIndex].SetActive(true);

            foreach (var x in ex)
            {
                x.SetActive(false);
            }
            ex[currentQuestionIndex].SetActive(true);

            // 重置氣球
            foreach (var balloon in balloons)
            {
                balloon.SetActive(true);
                var script = balloon.GetComponent<b1>();
                if (script != null)
                {
                    script.ResetBalloon();
                }
            }
        }

        //countdownText.transform.position = new Vector3(countdownPosition.x - 600f, countdownPosition.y+450f, countdownPosition.z);

        while (countdownTime >= 0 && !next)
        {
            countdownText.text = countdownTime.ToString();
            CheckResult();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        foreach (var balloon in balloons)
        {
            balloon.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f); // 過渡時間
    }

    void CheckResult()
    {
        bool anyBalloonHit = false;

        for (int i = 0; i < balloons.Length; i++)
        {
            var balloonScript = balloons[i].GetComponent<b1>();

            if (balloonScript != null && balloonScript.GetHP() == 0)
            {
                anyBalloonHit = true;
                if (IsCorrectAnswer(balloons[i]))
                {
                    countdownText.text = "Correct!";
                    score++;
                    CoinManager.currentGoldCoins += 2;
                }
                else
                {
                    
                    countdownText.text = "Wrong!";
                }
                next = true;
                break; // 確保只處理一次
            }
        }

        if (!anyBalloonHit && countdownTime < 1)
        {
           
            countdownText.text = "No Answer";
            next = true;
        }
    }

    bool IsCorrectAnswer(GameObject balloon)
    {
        string questionTag = questionObjects[currentQuestionIndex].tag;
        string balloonTag = balloon.tag;
        return questionTag == balloonTag;
    }
}
