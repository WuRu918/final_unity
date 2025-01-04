using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // 要生成的物體
    public float spawnInterval = 1.0f; // 生成間隔（秒）
    public Vector2 spawnArea; // 生成區域大小

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f; // 重置計時器
        }
    }

    void SpawnObject()
    {
        // 在生成區域內隨機選擇一個位置
        float x = transform.position.x;
        float y = transform.position.y + Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        Vector2 spawnPosition = new Vector2(x, y);

        // 生成物體
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        // 繪製生成區域範圍（僅用於場景視圖）
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, spawnArea.y, 0));
    }
}
