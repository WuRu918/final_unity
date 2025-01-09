using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flower;
using UnityEngine.SceneManagement;
using System;

public class npc_start : MonoBehaviour
{
    FlowerSystem fs;
    bool isInDialogMode = false;  // 判断是否在对话模式中
    bool isInRange = false;       // 判断玩家是否处于特定区域内



    void Start()
    {
        this.transform.tag = "npc";       
        // 确保这个对象有 Collider 并设置为 Trigger
        Collider2D col = this.GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
        {
            col.isTrigger = true; // 设置为触发器
        }
    }

    void Update()
    { 
        // 如果在对话模式下并且在范围内，按下Shift触发场景切换
        if (isInDialogMode && isInRange) 
        {
            if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                //fs.StartCoroutine(WaitForShiftAndLoadScene("YourSceneName"));
                // 场景加载
                SceneManager.LoadScene("answer_question");
                fs.RemoveDialog();

                // 对话结束，退出对话模式
                isInDialogMode = false;
            }          
        }

        // 只有在对话模式下，按下回车才触发下一步
        /*
        if (isInDialogMode && Input.GetKeyDown(KeyCode.Return))
        {
            fs.Next();
        }
        */
    }

    public void ChangeScene()
    {       
        try
        {
            // 检查是否已经存在 "default" 的 FlowerSystem
            fs = FlowerManager.Instance.GetFlowerSystem("default");
        }
        catch (Exception)
        {
            // 如果不存在，创建新的 FlowerSystem
            fs = FlowerManager.Instance.CreateFlowerSystem("default", false);
        }

        if (isInRange)  // 确保玩家在范围内
        {
            fs.SetupDialog();
            fs.ReadTextFromResource("npc");

            fs.RegisterCommand("set_dialog_true", (List<string> _params) => {
                isInDialogMode = true;
                Debug.Log("setting isInDialogMode " + isInDialogMode);

            });
            // 注册自定义指令
            fs.RegisterCommand("set_dialog_false", (List<string> _params) => {
                isInDialogMode = false;
            });

            Debug.Log("range. isInRange = " + isInRange);
            Debug.Log("isInDialogMode " + isInDialogMode);

            /*
            fs.RegisterCommand("load_scene", (List<string> _params) => {
                fs.StartCoroutine(WaitForShiftAndLoadScene(_params[0]));
            });
            */
        }
        Debug.Log("heere isInDialogMode " + isInDialogMode);
    }

    // 进入触发区域时
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家进入
        {
            // 玩家进入对话范围，启动对话
            isInRange = true;
            //Debug.Log("Player entered range. isInRange = " + isInRange);
            //Debug.Log("isInDialogMode " + isInDialogMode);
            ChangeScene();  // 立即启动对话
        }
    }

    // 离开触发区域时
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家离开
        {
            // 玩家离开范围，禁用Shift触发场景切换
            isInRange = false;
            Debug.Log("Player exited range. isInRange = " + isInRange);
        }
    }
}