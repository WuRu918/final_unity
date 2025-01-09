using UnityEngine;
using System.Collections;
using TMPro; // for TMP_Text

public class gamecontroller : MonoBehaviour
{
    public GameObject[] questionObjects; // 存放所有题目的 GameObject（每个题目对应一个 GameObject）
    public int currentQuestionIndex = 0; // 当前题目的索引
    public int score = 0; // 分数
    public GameObject[] balloons; // 拖入所有气球的 prefab

    public TMP_Text countdownText; // 倒计时文字
    private Vector3 countdownPosition;
    public int countdownTime = 3; // 倒计时秒数

    void Start()
    {
        // 隐藏所有照片（GameObject）
        foreach (var question in questionObjects)
        {
            question.SetActive(false);
        }

        // 显示第一个题目
        if (questionObjects.Length > 0)
        {
            questionObjects[0].SetActive(true);
        }

        countdownPosition = countdownText.transform.position;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
{
    while (true)
    {
        // 如果题目索引超出范围，表示游戏结束
        if (currentQuestionIndex >= questionObjects.Length)
        {
            countdownText.text = "Game Over!";
            yield return new WaitForSeconds(3f);
            Debug.Log("游戏结束，最终分数: " + score);
            break; // 结束游戏循环
        }

        // 重置气球
        ResetBalloons();

        // 倒计时
        yield return StartCoroutine(StartCountdown());

        // 显示当前题目
        UpdateQuestionObject();

        // 启动气球下落
        EnableBalloonMovement();

        // 等待玩家结算分数并确保所有气球都被隐藏
        yield return new WaitUntil(() => AreAllBalloonsHidden());

        // 等待玩家确认后继续游戏
        yield return new WaitForSeconds(1f);

        // 进入下一题
        currentQuestionIndex++;  // 切换到下一题
    }
}

    private IEnumerator StartCountdown()
    {
        countdownText.fontSize = 380;
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

            // 气球脚本
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

            // 显示气球
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

    public void CheckResult()
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
            if (pinkScript != null)
            {
                hp = pinkScript.GetHP();
            }
            else if (blackScript != null)
            {
                hp = blackScript.GetHP();
            }
            else if (ballloonScript != null)
            {
                hp = ballloonScript.GetHP();
            }
            else
            {
                Debug.LogError("Unknown balloon type: " + balloon.name);
                continue;
            }

            // 判断是否击中
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
            }

            // 如果气球未被隐藏，设置标志为 false
            if (!balloon.GetComponent<Renderer>().isVisible)
            {
                allBalloonsHidden = false;
            }
        }

        // 如果所有气球都已经隐藏，可以进行下一题
        if (allBalloonsHidden)
        {
            StartCoroutine(GameLoop());
        }
    }

    bool IsCorrectAnswer(GameObject balloon)
    {
        if (currentQuestionIndex < 0 || currentQuestionIndex >= questionObjects.Length)
        {
            Debug.LogError("Invalid question index: " + currentQuestionIndex);
            return false;
        }

        // 获取当前题目的 GameObject 的 tag
        string questionTag = questionObjects[currentQuestionIndex].tag;
        // 得到气球标签
        string balloonTag = balloon.tag;
        // 比较题目tag和被射掉的气球tag
        return questionTag == balloonTag;
    }

    bool AreAllBalloonsHidden()
    {
        foreach (var balloon in balloons)
        {
            if (balloon != null && balloon.GetComponent<Renderer>().isVisible)
            {
                return false; // 如果有气球仍然可见，返回 false
            }
        }
        return true; // 所有气球都已隐藏，返回 true
    }
}