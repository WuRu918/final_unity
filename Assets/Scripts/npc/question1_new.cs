using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using TMPro; // for TMP_Text

public class question1_new: MonoBehaviour
{
    public GameObject text; // 顯示遊戲說明
    bool isInRange = false;       // 判断玩家是否处于特定区域内



    void Start()
    {
        this.transform.tag = "npc";       
        // Collider 設為Trigger
        Collider2D col = this.GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
        {
            col.isTrigger = true; // 设置为触发器
        }
        text.SetActive(false);

    }

    void Update()
    { 
        if (isInRange) 
        {
            if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {       
                Debug.Log("here");   
                // 場景加載
                SceneManager.LoadScene("scene1");

            }          
        }
        
    }

    // 进入触发区域时
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家进入
        {
            // 玩家进入对话范围，启动对话
            isInRange = true;
            text.SetActive(true);
        }
    }

    // 离开触发区域时
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家离开
        {
            // 玩家離開範圍
            isInRange = false;
            text.SetActive(false);
        }
    }
}