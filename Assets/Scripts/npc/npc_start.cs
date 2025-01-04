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
        this.transform.tag = "npc" ;
              
    }

    void Update()
    { 
              
        if(Input.GetKeyDown(KeyCode.Return))
        {
            fs.Next();
        }

        if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)){
            SceneManager.LoadScene("answer_question");
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
        /*fs.RegisterCommand("load_scene",(List<string>_params)=>{
                SceneManager.LoadScene(_params[0]);
        });
        */
    }
    
}
