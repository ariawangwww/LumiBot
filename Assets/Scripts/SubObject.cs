using UnityEngine;

public class SubObject : MonoBehaviour
{
    public int answerValue; // 1, 2 或 3
    public AnswerObject parentObject; // 引用父对象


    private void Update()
    {
        // 检测鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            // 检查鼠标是否在当前Sprite上
            if (hit != null && hit.gameObject == gameObject)
            {
                parentObject.SetPlayerAnswer(answerValue);
                Debug.Log("选择答案: " + answerValue);
            }
        }
    }
}
