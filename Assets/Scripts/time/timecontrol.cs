using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timecontrol : MonoBehaviour
{
    public int m_seconds;                 //倒數計時經換算的總秒數
    public static bool enough_time = true;
    public int m_min;              //用於設定倒數計時的分鐘
    public int m_sec;              //用於設定倒數計時的秒數

    public Text m_timer;           //設定畫面倒數計時的文字
    public GameObject m_gameOver;  //設定 GAME OVER 物件
    public float gameOverDelay = 2f;     // 設置 Game Over 顯示的延遲時間
    private Coin_display coinDisplay;

    void Start()
    {
        StartCoroutine(Countdown());   //呼叫倒數計時的協程
        SceneManager.sceneLoaded += OnSceneLoaded;  // 註冊場景加載事件
        StartCoroutine(Countdown());   //呼叫倒數計時的協程
    
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        coinDisplay = GameObject.FindObjectOfType<Coin_display>();
        // 如果進入 samplescene 場景，重置計時器
        if (scene.name == "samplescene")
        {
            
            coinDisplay.coinText.text = "Coin: 0";
            ResetTimer();  // 呼叫 ResetTimer 方法
            StartCoroutine(Countdown());
        }

        // 如果進入 result 場景，隱藏倒數計時
        if (scene.name == "result" || scene.name == "start_2" || scene.name =="explaination") 
        {
            coinDisplay.gameObject.SetActive(false);
            StopAllCoroutines(); // 停止倒數計時
            m_timer.gameObject.SetActive(false);  // 隱藏倒數計時文字
        }
    }

    void ResetTimer()
    {
        m_seconds = (m_min * 60) + m_sec; // 重置秒數
        m_timer.text = string.Format("TIME: {0}:{1}", m_min.ToString("00"), m_sec.ToString("00"));
        enough_time = true;  // 重置計時狀態
    }

    IEnumerator Countdown()
    {
        m_timer.text = string.Format("TIME: {0}:{1}", m_min.ToString("00"), m_sec.ToString("00"));
        m_seconds = (m_min * 60) + m_sec;       //將時間換算為秒數

        while (m_seconds > 0)                   //如果時間尚未結束
        {
            yield return new WaitForSeconds(1); //等候一秒再次執行

            m_seconds--;                        //總秒數減 1
            m_sec--;                            //將秒數減 1

            if (m_sec < 0 && m_min > 0)         //如果秒數為 0 且分鐘大於 0
            {
                m_min -= 1;                     //先將分鐘減去 1
                m_sec = 59;                     //再將秒數設為 59
            }
            else if (m_sec < 0 && m_min == 0)   //如果秒數為 0 且分鐘大於 0
            {
                m_sec = 0;                      //設定秒數等於 0
            }
            m_timer.text = string.Format("TIME: {0}:{1}", m_min.ToString("00"), m_sec.ToString("00"));
        }

        yield return new WaitForSeconds(1);   //時間結束時，顯示 00:00 停留一秒
        m_gameOver.SetActive(true);           //時間結束時，畫面出現 GAME OVER
        yield return new WaitForSeconds(2f);
         m_gameOver.SetActive(false);
        Time.timeScale = 0;                   //時間結束時，控制遊戲暫停無法操作

        // 使用 WaitForSecondsRealtime 而不是 WaitForSeconds 來等待真實時間
        yield return new WaitForSecondsRealtime(gameOverDelay);
        enough_time = false;
        // 切換場景
        SceneManager.LoadScene("result");
    }
}
