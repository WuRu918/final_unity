using UnityEngine;
using Platformer.Mechanics;


public class PlayerPositionLoader : MonoBehaviour
{
    void Start()
    {
        // 如果有保存位置，移動玩家到該位置
        if (PlayerController.hasSavedPosition)
        {
            transform.position = PlayerController.savedPosition;
        }
        else
        {
            Debug.Log("沒有保存的位置，從場景預設位置開始。");
        }
    }
}
