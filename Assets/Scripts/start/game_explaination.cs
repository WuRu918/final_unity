using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;  // 引入 SceneManager 命名空間

public class game_explaination : MonoBehaviour
{
    void Start(){

    }
    
    void Updata(){

    }

    public void Button(){
        print("GameStart");
        SceneManager.LoadScene("SampleScene");
    }
    


}
