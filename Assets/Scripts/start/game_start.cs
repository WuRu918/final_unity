using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;  // 引入 SceneManager 命名空間
using UnityEngine.EventSystems;  // 引入 EventSystems 用於觸發事件

public class game_explaination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonRectTransform;
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1f);  // 設置放大比例

    void Start(){

    }
    
    void Updata(){

    }

    public void Button_to_explaination(){
        
        SceneManager.LoadScene("explaination");
    }

    // 當滑鼠進入按鈕時，放大
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonRectTransform.localScale = enlargedScale;
    }

    // 當滑鼠離開按鈕時，恢復大小
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonRectTransform.localScale = normalScale;
    }
    
}
