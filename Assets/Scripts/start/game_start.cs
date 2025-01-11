using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameExplaination : MonoBehaviour
{
    public AudioSource audioSource; // 將 AudioSource 拖入編輯器

    public void Button_to_explaination()
    {
        // 啟動協程
        StartCoroutine(PlayAudioAndSwitchScene());
    }

    private IEnumerator PlayAudioAndSwitchScene()
    {
        var audio = this.GetComponent<AudioSource>();
        // 播放音效
        audio.Play();

        // 等待音效播放完成
        yield return new WaitForSeconds(audio.clip.length);

        // 切換場景
        SceneManager.LoadScene("explaination");
    }
}