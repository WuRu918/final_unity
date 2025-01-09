using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1 :  MonoBehaviour
{
    public int hp = 0;
    public int hp_max = 2;
    public GameObject blood;
    public GameObject blood_base;
    public Animator bulloonAni;

    private gamecontroller gameController;

    void Start()
    {
        gameController = FindObjectOfType<gamecontroller>();
        hp=hp_max;
    }

    public int GetHP()
    {
        return hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && hp>0)
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
            gameController.CheckResult();
            gameObject.SetActive(false);

            //animation
            //bulloonAni.SetTrigger("boom_trigger");
            var audio = this.GetComponent<AudioSource>();

            // 播放音效
            audio.Play();

        }
    }

    public void ResetBalloon()
    {
        // 随机生成新位置
        /*
        float randomX = Random.Range(-7.85f, 1.70f);
        this.transform.position = new Vector3(randomX, 1.70f, this.transform.position.z);
        */

        // 恢复血量
        hp = hp_max;

        // 恢复血条
        blood.transform.localScale = new Vector3(1f, blood.transform.localScale.y, blood.transform.localScale.z);

        // 重新启用气球
        gameObject.SetActive(true);
    }   

    
}
