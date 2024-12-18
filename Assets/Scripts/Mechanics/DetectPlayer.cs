using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject QuestionUi;
    // 當有物件進入觸發區域時執行
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 檢查進入的物件是否標籤為 "Player"
        if (other.CompareTag("Player"))
        {
            // 在此處添加觸發時的行為
            Debug.Log("Player entered the trigger zone!");
            OpenQuestion();
        }
    }

    // 範例行為方法
    private void OpenQuestion()
    {
        QuestionUi.SetActive(true);
    }
}