using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballloon : MonoBehaviour
{
    public int hp = 0;
    public int hp_max = 0;
    public GameObject blood;
    public GameObject blood_base;
    public Animator bulloonAni;
    public bool canPlay = false;
    private Rigidbody2D rb; // 參考 Rigidbody2D 組件


    void Start()
    {
        InitializeBalloon();
    }


    // 初始化、重置氣球狀態
    public void ResetBalloon()
    {
        InitializeBalloon();
    }

    public int GetHP()
    {
        return hp;
    }



    void Update()
    {
        if(canPlay == true)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hp -= 1;
            Destroy(other.gameObject);

            // 調整血條
            float percent = (float)hp / (float)hp_max;
            float clampedPercent = Mathf.Max(percent, 0f);  // 確保血條百分比不會小於 0
            blood.transform.localScale = new Vector3(clampedPercent, blood.transform.localScale.y, blood.transform.localScale.z);
        }
       
        if (hp <= 0)
        {
            bulloonAni.SetTrigger("boom_trigger");
            var audio = this.GetComponent<AudioSource>();
            // 播放音效
            audio.Play();
            HideBalloon();

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            HideBalloon();

        }
    }



    private void InitializeBalloon()
    {
        //隨機生成
        float randomX = Random.Range(-7.85f, 1.70f);
        this.transform.position = new Vector3(randomX, 6.22f, this.transform.position.z);

        hp_max = 2; // 氣球最大血量
        hp = hp_max; // 初始血量

        //重置氣球血條
        if (blood != null)
        {
            blood.transform.localScale = new Vector3(1, blood.transform.localScale.y, blood.transform.localScale.z);
        }

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        canPlay = false;
        
    }   

    void HideBalloon()
    {
        // 隱藏氣球的 Renderer 使其不可見
        Renderer balloonRenderer = GetComponent<Renderer>();
        if (balloonRenderer != null)
        {
            balloonRenderer.enabled = false;  // 隱藏渲染器
        }

        // 隱藏血條的 Renderer
        if (blood != null)
        {
            Renderer bloodRenderer = blood.GetComponent<Renderer>();
            Renderer bloodRenderer_base = blood_base.GetComponent<Renderer>();
            if (bloodRenderer != null)
            {
                bloodRenderer.enabled = false;  // 隱藏血條
                bloodRenderer_base.enabled = false;
            }
        }

        // 禁用物理效果，停止物理運算
        if (rb != null)
        {
            rb.simulated = false;  
        }

        // 禁用碰撞檢測
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;  // 禁用碰撞檢測
        }

        // 播放爆炸動畫時，停止其他動畫，並確保動畫回到初始狀態
        if (bulloonAni != null)
        {
            bulloonAni.SetTrigger("return"); // 觸發爆炸動畫
        }
    }
}