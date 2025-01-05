using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballloon : MonoBehaviour
{
    public int hp = 0;
    public int hp_max = 0;
    public GameObject blood;

    // 初始重生位置
    private Vector3 initialPosition;

    void Start()
    {
        hp_max = 2; // 設置最大血量
        hp = hp_max; // 初始化當前血量
        initialPosition = this.transform.position; // 記錄初始位置
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
            StartCoroutine(RespawnBalloon());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            StartCoroutine(RespawnBalloon());
        }
    }

    IEnumerator RespawnBalloon()
    {
        // 隱藏當前氣球，等待重生
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        // 恢復初始位置與狀態
        this.transform.position = initialPosition;
        hp = hp_max;

        // 恢復血條縮放
        blood.transform.localScale = new Vector3(1, blood.transform.localScale.y, blood.transform.localScale.z);

        // 顯示氣球
        this.gameObject.SetActive(true);
    }
}