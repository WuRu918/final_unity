﻿using UnityEngine;

public class parallax : MonoBehaviour
{
    Transform cam;
    Vector3 prevTargetPos;  // 上一幀的相機位置
    Material mat;
    float distance;
    float currentSpeed;
    public Transform target;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;  // 正常速度
    public float idleSpeed = 0.05f;  // 相機靜止時的降低速度

    void Start()
    {
        cam = Camera.main.transform;
        prevTargetPos = target.position;  // 初始化為相機當前位置
        mat = GetComponent<Renderer>().material;
        currentSpeed = speed;  // 默認為正常速度
    }

    void Update()
    {
        // 檢測相機是否移動
        if (target.position != prevTargetPos)
        {
            currentSpeed = speed;  // 相機在移動，使用正常速度
        }
        else
        {
            currentSpeed = idleSpeed;  // 相機靜止，使用降低速度
        }

        // 更新紋理偏移
        distance += Time.deltaTime * currentSpeed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);

        // 確保物件位置同步更新
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        // 儲存相機的當前位置以供下一幀檢測
        prevTargetPos = target.position;
    }
}
