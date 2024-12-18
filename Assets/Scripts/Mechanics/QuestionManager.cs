using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    public int Score;
    public Text scoreText;

    public GameObject QuestionUi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AnswerCorrect()
    {
        Score++;
        Debug.Log("Correct");
        QuestionUi.SetActive(false);
        UpdateText();
    }
    public void AnswerWrong()
    {
        Debug.Log("Wrong");
        QuestionUi.SetActive(false);
    }
    public void UpdateText()
    {
        scoreText.text = "Score:"+Score.ToString();
    }
}
