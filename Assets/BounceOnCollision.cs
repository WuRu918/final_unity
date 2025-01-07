using UnityEngine;

public class BounceOnTrigger : MonoBehaviour
{
    public float invincibleTimer = 0.0f;
    public float invincibleDuration = 1.0f;
    public float bounceForce = 5.0f;  // 反彈強度
    public Rigidbody2D characterRigidbody;  // 控制角色反彈的 Rigidbody2D

    private bool isInvincible = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 確保是其他物體進入角色的 trigger 區域
        if (other.GetComponent<Rigidbody2D>() != null && !isInvincible)
        {
            // 計算反彈的方向（角色與物體的相對位置）
            Vector2 bounceDirection = transform.position - other.transform.position;
            bounceDirection.Normalize();  // 確保方向向量是單位向量

            // 在反彈方向中加入 y 軸反彈
            bounceDirection.y = Mathf.Abs(bounceDirection.y); // 保證 y 軸方向的反彈是正向的

            // 使用 Rigidbody2D 的 position 更新角色位置
            if (characterRigidbody != null)
            {
                // 計算新的反彈位置
                Vector2 newPosition = characterRigidbody.position + bounceDirection * bounceForce;

                // 直接設置 Rigidbody2D 的 position
                characterRigidbody.position = newPosition;
            }
            isInvincible = true;
            invincibleTimer = invincibleDuration;
        }
    }

    private void Update()
    {
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
}
