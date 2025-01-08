using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballloon : MonoBehaviour
{
    public int hp = 0;
    public int hp_max = 0;
    public GameObject blood;
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
            blood.transform.localScale = new Vector3(percent, blood.transform.localScale.y, blood.transform.localScale.z);
        }

        if (hp <= 0)
        {
            bulloonAni.SetTrigger("boom_trigger");
            var audio = this.GetComponent<AudioSource>();
            // 播放音效
            audio.Play();
            Destroy(this.gameObject, audio.clip.length);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
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
}