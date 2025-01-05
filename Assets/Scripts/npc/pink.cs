using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pink : MonoBehaviour
{
    public int hp = 0;
    public int hp_max = 0;
    public GameObject blood;
    public Animator bulloonAni;

    


    void Start()
    {
        // 隨機生成 x 位置
        float randomX = Random.Range(-7.85f, 7.85f);
        hp_max = 2; // 設置最大血量
        hp = hp_max; // 初始化當前血量
        
        // 設置新位置
        this.transform.position = new Vector3(randomX, 4.28f, this.transform.position.z);
        
    }

    void Update()
    {
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
}