using UnityEngine;

public class FadeBlock : MonoBehaviour
{
    public float fadeSpeed = 1f; // 透明度變化速度
    private SpriteRenderer spriteRenderer;
    private bool isFading = false; // 是否正在淡化
    private float targetAlpha = 1f; // 目標透明度

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("需要在 GameObject 上添加 SpriteRenderer！");
        }
    }

    void Update()
    {
        // 如果透明度與目標透明度不同，逐漸變化
        if (Mathf.Abs(spriteRenderer.color.a - targetAlpha) > 0.01f)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保是角色進入
        {
            targetAlpha = 0f; // 目標透明度設置為完全透明
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保是角色離開
        {
            targetAlpha = 1f; // 目標透明度設置為完全不透明
        }
    }
}
