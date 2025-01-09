using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flower;
using UnityEngine.SceneManagement;
using System;

public class question2 : MonoBehaviour
{
    FlowerSystem fs;
    bool isInDialogMode2 = false;  // 判断是否在对话模式中
    bool isInRange2 = false;       // 判断玩家是否处于特定区域内



    void Start()
    {
        this.transform.tag = "npc";       
        // 确保这个对象有 Collider 并设置为 Trigger
        Collider2D col = this.GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
        {
            col.isTrigger = true; // 设置为触发器
        }

        fs.RegisterCommand("set_dialog_true2", (List<string> _params) => {
                isInDialogMode2 = true;
                Debug.Log("setting isInDialogMode " + isInDialogMode2);
            });

        fs.RegisterCommand("set_dialog_false2", (List<string> _params) => {
                isInDialogMode2 = false;
        });

    }

    void Update()
    { 
        // 如果在对话模式下并且在范围内，按下Shift触发场景切换
        if (isInDialogMode2 && isInRange2) 
        {
            if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                //fs.StartCoroutine(WaitForShiftAndLoadScene("YourSceneName"));
                // 场景加载
                SceneManager.LoadScene("scene2");
                fs.RemoveDialog();

                // 对话结束，退出对话模式
                isInDialogMode2 = false;
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

        if (isInRange2)  // 确保玩家在范围内
        {
            fs.SetupDialog();
            fs.ReadTextFromResource("question2");

            
        
            Debug.Log("range. isInRange2= " + isInRange2);
            Debug.Log("isInDialogMode2" + isInDialogMode2);

            /*
            fs.RegisterCommand("load_scene", (List<string> _params) => {
                fs.StartCoroutine(WaitForShiftAndLoadScene(_params[0]));
            });
            */
        }
        Debug.Log("heere isInDialogMode2" + isInDialogMode2);
    }

    // 进入触发区域时
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家进入
        {
            // 玩家进入对话范围，启动对话
            isInRange2 = true;
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
            isInRange2 = false;
            Debug.Log("Player exited range. isInRange2= " + isInRange2);
        }
    }
}