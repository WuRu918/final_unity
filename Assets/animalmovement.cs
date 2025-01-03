using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // 动物移动速度
    private Camera mainCamera;

    private float leftBoundary;
    private float rightBoundary;

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 更新边界
        UpdateBoundaries();

        // 动物向左移动
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 如果超出左边界，将动物移到右边界
        if (transform.position.x < leftBoundary)
        {
            transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);
        }
        
    }

    void UpdateBoundaries()
    {
        // 获取相机左下角和右上角的世界坐标
        Vector3 leftBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - mainCamera.transform.position.z));
        Vector3 rightTop = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, transform.position.z - mainCamera.transform.position.z));

        // 设置左右边界
        leftBoundary = leftBottom.x;
        rightBoundary = rightTop.x;
    }

    void OnDrawGizmos()
    {
        // 方便调试，在场景中画出边界
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBoundary, -10, 0), new Vector3(leftBoundary, 10, 0));
        Gizmos.DrawLine(new Vector3(rightBoundary, -10, 0), new Vector3(rightBoundary, 10, 0));
    }
}
