﻿using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f; // 移動速度
    public float destroyBoundary = 20.0f; // 超過此範圍銷毀物體
    public float fadeSpeed = 1.0f; // 透明度過渡的速度
    private SpriteRenderer spriteRenderer; // 物體的 SpriteRenderer 組件
    private bool isFading = false; // 是否正在變透明

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 獲取 SpriteRenderer
    }

    void Update()
    {
        // 向右移動
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (speed > 0)
        {
            if (transform.position.x > destroyBoundary)
            {
                if (!isFading)
                {
                    isFading = true;
                    StartCoroutine(FadeOut()); // 開始透明過渡
                }
            }
        }
        else
        {
            if (transform.position.x < destroyBoundary)
            {
                if (!isFading)
                {
                    isFading = true;
                    StartCoroutine(FadeOut()); // 開始透明過渡
                }
            }
        }
        // 如果物體超出範圍，開始變透明
        
    }

    // 漸變透明度的協程
    IEnumerator FadeOut()
    {
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // 透明顏色

        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            // 計算過渡顏色
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一幀
        }

        // 確保完全透明後銷毀物體
        spriteRenderer.color = targetColor;
        Destroy(gameObject);
    }
}
