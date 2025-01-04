using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator; // 角色的 Animator
    private Rigidbody2D rb;  // 角色的剛體（2D 遊戲使用 Rigidbody2D，3D 遊戲用 Rigidbody）

    void Start()
    {
        // 獲取角色的剛體組件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 計算角色的移動速度（可以根據實際情況更改）
        float velocityX = rb.linearVelocity.magnitude;

        // 將速度傳遞給 Animator 的 Speed 參數
        animator.SetFloat("velocityX", velocityX);
    }
}
