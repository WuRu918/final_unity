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
    public int countdownTime = 3; // 倒计时秒数
    public GameObject[] questionObjects;
    public GameObject[] button;
    public Sprite[] buttonImages1; // 存储所有按钮图片，根据题目序号设置
    public Sprite[] buttonImages2; // 存储所有按钮图片，根据题目序号设置
    public Sprite[] buttonImages3; // 存储所有按钮图片，根据题目序号设置

    private int currentQuestionIndex = 0; // 当前题目索引
    public int score = 0;  // 添加分数变量

    void Start()
    {
        question.text = "Select the correct option to earn coins (2 coins per question)";
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
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(3f);

        // 开始游戏循环
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

            // 倒计时
            yield return StartCoroutine(StartCountdown());
            question.text = "";
            // 更新题目
            UpdateQuestionObject();
            currentQuestionIndex++;
        }
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
            questionObjects[currentQuestionIndex].transform.position = new Vector3(1200f,1300f,0);
        }
        else
        {
            Debug.LogWarning("No more questions to display or invalid index.");
        }

        // 显示所有按钮
        foreach (var buttonObject in button)
        {
            buttonObject.SetActive(true);
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

    private IEnumerator StartCountdown()
    {
        countdownTime = 3;
        countdownText.fontSize = 380;

        while (countdownTime >= 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        yield return new WaitForSeconds(1f);
    }
}