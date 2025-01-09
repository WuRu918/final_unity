using UnityEngine;
using System.Collections;
using TMPro; // for TMP_Text
using UnityEngine.SceneManagement;

public class gamecontroller : MonoBehaviour
{
    public GameObject image;
    public GameObject[] questionObjects; // 存放所有题目的 GameObject（
    public int currentQuestionIndex = 0; // 當前題號
    public int score = 0; // 分数
    public GameObject[] balloons; // 三顆氣球

    public TMP_Text countdownText; // 倒數計時
    private Vector3 countdownPosition;
    public int countdownTime = 3; // 秒數
    public GameObject door;

    void Start()
    {
        // 隐藏所有照片（GameObject）
        foreach (var question in questionObjects)
        {
            question.SetActive(false);
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
        StartCoroutine(GameLoop());
    }
    
    IEnumerator GameLoop()
    {   
        while (currentQuestionIndex <= questionObjects.Length)
        {
            Debug.Log("當前題號"+currentQuestionIndex);
            // 如果题目索引超出范围，表示游戏结束
            if (currentQuestionIndex >= questionObjects.Length)
            {
                countdownText.text = "Game Over!";
                yield return new WaitForSeconds(3f);
                Debug.Log("游戏结束，最终分数: " + score);
                door.SetActive(true);
            }

            // 重置气球
            ResetBalloons();

            // 倒计时
            yield return StartCoroutine(StartCountdown());

            // 显示当前题目
            UpdateQuestionObject();

            // 启动气球下落
            EnableBalloonMovement();

            // 等待玩家确认后继续游戏
            yield return new WaitForSeconds(1f);

            // 进入下一题
            currentQuestionIndex++;  // 切换到下一题
        }
    }

    //碰到門就可以離開
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            //StartCoroutine(PlayAnimationAndWait());
            SceneManager.LoadScene("SampleScene");  
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownText.fontSize = 300;
        countdownText.transform.position = new Vector3(countdownPosition.x - 200f, countdownPosition.y, countdownPosition.z);
        countdownText.text = "Ready!";
        yield return new WaitForSeconds(1f);

        // 倒计时
        while (countdownTime > 0)
        {
            countdownText.transform.position = new Vector3(countdownPosition.x, countdownPosition.y, countdownPosition.z);         
            countdownText.fontSize = 500;
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.transform.position = new Vector3(countdownPosition.x - 200f, countdownPosition.y, countdownPosition.z);
        countdownText.fontSize = 380;
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // 清空倒计时文字
        countdownText.text = "";

        // **按顺序显示题目图片**
        UpdateQuestionObject();

        // 更新索引，准备进入下一题
        /*if (currentQuestionIndex < questionObjects.Length - 1)
        {
            currentQuestionIndex++;  // 只有在正确显示题目后才增加
        }
        */

        // 启动气球下落
        EnableBalloonMovement();
    }

    void UpdateQuestionObject()
    {
        // 确保当前的题目显示，其他题目隐藏
        if (currentQuestionIndex >= 0 && currentQuestionIndex < questionObjects.Length)
        {
            // 隐藏所有题目
            foreach (var question in questionObjects)
            {
                question.SetActive(false);
            }

            Debug.Log("here");
            // 只显示当前的题目
            questionObjects[currentQuestionIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No more questions to display or invalid index.");
        }
    }

    void ResetBalloons()
    {
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue; // 确保气球实例存在
            var resettable = balloon.GetComponent<IResettable>();
            if (resettable != null)
            {
                resettable.ResetBalloon(); // 重置气球位置和状态
            }
        }
    }

    void EnableBalloonMovement()
    {
        // 所有气球
        foreach (var balloon in balloons)
        {
            if (balloon == null) continue;

            // 獲得氣球腳本
            var ballloonScript = balloon.GetComponent<ballloon>();
            var pinkScript = balloon.GetComponent<pink>();
            var blackScript = balloon.GetComponent<black>();

            // canPlay ＝ true
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

            // Rigidbody2D ＝ Dynamic
            var rb = balloon.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                print("Rigidbody2D is missing on: " + balloon.name);
            }

            // 顯示氣球
            Renderer renderer = balloon.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;  
            }

            // 恢复物理和碰撞
            Rigidbody2D balloonRb = balloon.GetComponent<Rigidbody2D>();
            if (balloonRb != null)
            {
                balloonRb.simulated = true; 
            }

            Collider2D collider = balloon.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;  
            }
        }
    }

    public IEnumerator CheckResult()
    {
        bool allBalloonsHidden = true;

        foreach (var balloon in balloons)
        {
            if (balloon == null)
            {
                Debug.LogError("Balloon is null");
                continue;
            }

            var pinkScript = balloon.GetComponent<pink>();
            var blackScript = balloon.GetComponent<black>();
            var ballloonScript = balloon.GetComponent<ballloon>();

            int hp = -1;
            

            // 获取气球的HP
            if (pinkScript != null)//pink balloon
            {
                hp = pinkScript.GetHP();
            }
            else if (blackScript != null)//black balloon
            {
                hp = blackScript.GetHP();
            }
            else if (ballloonScript != null)//orange balloon
            {
                hp = ballloonScript.GetHP();
            }
            else
            {
                Debug.LogError("Unknown balloon type: " + balloon.name);
                continue;
            }

            // 判断該氣球是否被擊中
            if (hp <= 0) // 如果气球血量为0或以下
            {
                // 判断是否答对
                if (IsCorrectAnswer(balloon))
                {
                    score += 1;
                    countdownText.text = "Correct!";
                    Debug.Log("Score increased. Current Score: " + score);
                }
                else
                {
                    countdownText.text = "Wrong!";
                }

                // 隐藏气球
                if (pinkScript != null)
                {
                    pinkScript.HideBalloon();
                }
                if (blackScript != null)
                {
                    blackScript.HideBalloon();
                }
                if (ballloonScript != null)
                {
                    ballloonScript.HideBalloon();
                }
                allBalloonsHidden = false;
                 yield return new WaitForSeconds(2f);
                countdownText.text = "Next Question !";
                StartCoroutine(GameLoop());
            }
        }

    }

    bool IsCorrectAnswer(GameObject balloon)
    {
        /*
        if (currentQuestionIndex < 0 || currentQuestionIndex >= questionObjects.Length)
        {
            Debug.LogError("Invalid question index: " + currentQuestionIndex);
            return false;
        }
        */
        Debug.Log("當前題號"+currentQuestionIndex);
        // 获取当前题目的 GameObject 的 tag
        string questionTag = questionObjects[currentQuestionIndex].tag;
        // 得到氣球標籤
        string balloonTag = balloon.tag;
        // 比较题目tag和被射掉的气球tag
        return questionTag == balloonTag;
    }

}