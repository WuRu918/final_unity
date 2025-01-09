using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro; // for TMP_Text
using UnityEngine.UI;  // for Button and Image
using System;

public class npc2 : MonoBehaviour
{
    public TMP_Text question; // 顯示遊戲說明
    public TMP_Text countdownText; // 倒數
    public int countdownTime = 5; // 倒计时秒数
    public GameObject[] questionObjects;
    public GameObject[] button;
    public Sprite[] buttonImages1; // 存储所有按钮图片，根据题目序号设置
    public Sprite[] buttonImages2; // 存储所有按钮图片，根据题目序号设置
    public Sprite[] buttonImages3; // 存储所有按钮图片，根据题目序号设置

    //scene
    public GameObject door;
    public Animator animator;
    public string animationName;
    public float animationDuration; // 动画的时长

    private int currentQuestionIndex = 0; // 当前题目索引
    public int score = 0;  // 添加分数变量

    void Start()
    {
        //question.text = "Select the correct option to earn coins (2 coins per question)";
        countdownText.fontSize = 100;
        countdownText.text = "Ready!";

        // 隐藏所有题目
        foreach (var questionObject in questionObjects)
        {
            questionObject.SetActive(false);
        }

        // 隐藏所有按钮
        foreach (var buttonObject in button)
        {
            buttonObject.SetActive(false);
        }
        
        // Start a coroutine instead of using yield directly in Start
        StartCoroutine(DelayedStart());

        //
        door.SetActive(false);
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(3f);

        // 开始游戏循环
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (currentQuestionIndex < questionObjects.Length)
        {
            // 如果题目索引超出范围，表示游戏结束
            if (currentQuestionIndex >= questionObjects.Length)
            {
                // 隐藏所有题目
            foreach (var questionObject in questionObjects)
            {
                questionObject.SetActive(false);
            }

            // 隐藏所有按钮
            foreach (var buttonObject in button)
            {
                buttonObject.SetActive(false);
            }
                countdownText.text = "Game Over!";
                yield return new WaitForSeconds(3f);
                countdownText.text ="";
                question.text = "Go Left to return game";

                door.SetActive(true);

                Debug.Log("游戏结束，最终分数: " + score);
                
                break; // 结束游戏循环
            }

            // 倒计时
            yield return StartCoroutine(StartCountdown());
            question.text = "";
            // 更新题目
            UpdateQuestionObject();
            currentQuestionIndex++;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayAnimationAndWait());
            SceneManager.LoadScene("SampleScene");  
        }
    }

    IEnumerator PlayAnimationAndWait()
    {
        animator.Play(animationName); 
        yield return new WaitForSeconds(animationDuration); 
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
            questionObjects[currentQuestionIndex].transform.position = new Vector3(1200f,1270f,0);
        }
        else
        {
            Debug.LogWarning("No more questions to display or invalid index.");
        }

        // 显示所有按钮
        foreach (var buttonObject in button)
        {
            buttonObject.SetActive(true);
            buttonObject.GetComponent<Button>().interactable = true; 
        }

        // 更新按钮图片
        if (currentQuestionIndex == 0)
        {
            button[0].GetComponent<Image>().sprite = buttonImages1[0];
            button[1].GetComponent<Image>().sprite = buttonImages1[1];
            button[2].GetComponent<Image>().sprite = buttonImages1[2];
            button[3].GetComponent<Image>().sprite = buttonImages1[3];
        }

        if (currentQuestionIndex == 1)
        {
            button[0].GetComponent<Image>().sprite = buttonImages2[0];
            button[1].GetComponent<Image>().sprite = buttonImages2[1];
            button[2].GetComponent<Image>().sprite = buttonImages2[2];
            button[3].GetComponent<Image>().sprite = buttonImages2[3];
        }

        if (currentQuestionIndex == 2)
        {
            button[0].GetComponent<Image>().sprite = buttonImages3[0];
            button[1].GetComponent<Image>().sprite = buttonImages3[1];
            button[2].GetComponent<Image>().sprite = buttonImages3[2];
            button[3].GetComponent<Image>().sprite = buttonImages3[3];
        }
    }

    private int selectedButtonIndex = -1; // 用于记录按下的按钮索引

    private IEnumerator StartCountdown()
    {
        countdownTime = 5;
        countdownText.fontSize = 250;

        while (countdownTime >0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        
        //一開始的顯示
        if(currentQuestionIndex == 0)
        {
            countdownText.fontSize = 150;
            countdownText.text = "Go ! ";
        }

        if(currentQuestionIndex > 0)
        {
            if (selectedButtonIndex == -1)
            {
                foreach (var buttonObject in button)
                {
                    buttonObject.GetComponent<Button>().interactable = false; 
                }
                countdownText.fontSize = 100;
                countdownText.text = "Wrong";
                yield return new WaitForSeconds(1f);
                countdownText.text = "Next Question";
                ResetButtons();
            }
            else
            {
                GameObject selectedButton = button[selectedButtonIndex];
                //其他按鈕倒數完就不能再按
                foreach (var buttonObject in button)
                {
                    if(selectedButton !=buttonObject)
                    {
                        buttonObject.GetComponent<Button>().interactable = false; 
                    }
                }

                //後面答題的顯示
                if(currentQuestionIndex > 0)
                {
            
                    if(IsCorrect(selectedButton))
                    {
                        countdownText.fontSize = 100;
                        countdownText.text = "Correct ";
                        yield return new WaitForSeconds(1f);
                        countdownText.text = "Next Question";
                        
                    }else
                    {
                        countdownText.text = "Wrong";
                        yield return new WaitForSeconds(1f);
                        countdownText.text = "Next Question";
                    }
                    
                }
            }
        }
        
        
        yield return new WaitForSeconds(1f);
    }

    bool  IsCorrect(GameObject button)
    {
        string questionTag = questionObjects[currentQuestionIndex].tag;
        string buttonTag = button.tag;
        return questionTag == buttonTag;
    }

    // 在倒數結束前紀錄選擇了什麼按鈕
    public void OnButtonClicked(int buttonIndex)
    {
        selectedButtonIndex = buttonIndex; 
        // 打印被点击的按钮的索引
        Debug.Log("Button " + buttonIndex + " clicked!");

        // 你可以在此做一些逻辑，比如处理分数或切换场景等
                   
    }

    void ResetButtons()
    {
        // 重置按钮的视觉状态
        foreach(var buttonObject in button)
        {
            Image buttonImage = buttonObject.GetComponent<Image>();
            if (buttonImage != null)
            {
                // 你可以在这里设置默认的图片或状态
                buttonImage.color = Color.white; // 恢复默认颜色
            }
        }
        
        // 重置选中按钮的索引
        selectedButtonIndex = -1;
    }
}