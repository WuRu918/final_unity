using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;  // 引入 SceneManager 命名空間


public class explaination : MonoBehaviour
{
    

    void Start(){

    }
    
    void Updata(){

    }

    public void Button_to_game(){
        
        SceneManager.LoadScene("SampleScene");
    }

    
    
}
