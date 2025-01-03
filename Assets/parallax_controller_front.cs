using UnityEngine;

public class parallax_controller_front : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 10)]
    public float parallaxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            float zDistance = backgrounds[i].transform.position.z - cam.position.z;

            // 更新最遠距離，包括前景和背景
            if (Mathf.Abs(zDistance) > Mathf.Abs(farthestBack))
            {
                farthestBack = zDistance;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            float zDistance = backgrounds[i].transform.position.z - cam.position.z;

            // 根據 Z 軸距離計算速度，支持正負距離
            backSpeed[i] = zDistance > 0
                ? 1 - (zDistance / farthestBack)   // 背景：速度更慢
                : 1 + (Mathf.Abs(zDistance) / farthestBack); // 前景：速度更快
        }
    }


    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;

        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;

            if (backgrounds[i].transform.position.z < cam.position.z)
            {
                mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
            }
            else
            {
                mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
            }
        }
    }
}

