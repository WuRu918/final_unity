using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Flower;
using UnityEngine.SceneManagement;
using System;

public class npc_start : MonoBehaviour
{
    FlowerSystem fs;

    void Start()
    {
        this.transform.tag = "npc";       
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fs.Next();
        }
    }

    public void ChangeScene()
    {       
        try
        {
            // 檢查是否已經存在 "default" 的 FlowerSystem
            fs = FlowerManager.Instance.GetFlowerSystem("default");
        }
        catch (Exception)
        {
            // 如果不存在，創建新的 FlowerSystem
            fs = FlowerManager.Instance.CreateFlowerSystem("default", false);
        }

        fs.SetupDialog();
        fs.ReadTextFromResource("npc");


        // 註冊自定義指令
        fs.RegisterCommand("load_scene", (List<string> _params) => {
            fs.StartCoroutine(WaitForShiftAndLoadScene(_params[0]));
        });
    }
    
    IEnumerator WaitForShiftAndLoadScene(string sceneName)
    {
        Debug.Log($"等待 Shift 鍵以切換到場景: {sceneName}");
        
        // 持續等待直到按下 Shift 鍵
        while (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            yield return null; // 等待下一幀
        }
        
        Debug.Log($"Shift 鍵已按下，切換到場景: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
