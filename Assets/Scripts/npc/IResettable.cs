using UnityEngine;

// 定義接口
public interface IResettable
{
    void ResetBalloon(); // 重置氣球狀態
    int GetHP(); // 獲取氣球hp
}
