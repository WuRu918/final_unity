using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class con_position : MonoBehaviour
{
    public GameObject target; // 要跟隨的物件

    void Update()
    {
        if (target != null)
        {
            // 使用 npc 的 Transform 組件來訪問位置
            transform.position = target.transform.position;
        }
    }
}