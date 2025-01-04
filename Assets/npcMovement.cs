using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // 移動速度
    public float changeDirectionInterval = 3.0f; // 改變方向的時間間隔
    public float stopChance = 0.3f; // 停止的機會（0.0 ~ 1.0）

    private Vector3 targetDirection; // 目標移動方向
    private float timer = 0f; // 計時器
    private bool isMoving = true; // 是否正在移動

    private Animator animator; // Animator 組件
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;

        // 其他初始化邏輯
        SetNewRandomDirection();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            // 持續移動
            transform.Translate(targetDirection * moveSpeed * Time.deltaTime);
        }

        // 更新動畫參數
        animator.SetBool("isWalking", isMoving);

        // 計時器更新
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            timer = 0f; // 重置計時器

            // 隨機決定是否停止或改變方向
            if (Random.value < stopChance)
            {
                isMoving = false; // 停止移動
            }
            else
            {
                isMoving = true; // 繼續移動
                SetNewRandomDirection(); // 設置新的隨機方向
            }
        }

        // 更新朝向（根據移動方向）
        UpdateFacingDirection();
    }

    void SetNewRandomDirection()
    {
        // 隨機選擇一個左右的方向（只在 x 軸上）
        float randomX = Random.Range(-1f, 1f); // 隨機生成 x 軸方向
        targetDirection = new Vector3(randomX, 0, 0).normalized; // y 軸設為 0，只在 x 軸上移動
    }

    void UpdateFacingDirection()
    {
        // 根據 x 軸移動方向更新朝向，但保留其他軸的比例
        if (targetDirection.x > 0)
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); // 面向右
        }
        else if (targetDirection.x < 0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z); // 面向左
        }
    }
}
