using UnityEngine;

public class SubObject : MonoBehaviour
{
    public int answerValue; // 1, 2 或 3
    public AnswerObject parentObject; // 引用父对象
    private SpriteRenderer spriteRenderer; // 引用SpriteRenderer
    private Color originalColor; // 原始颜色
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 保存原始颜色
        parentObject = GetComponentInParent<AnswerObject>();
    }


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
                
            }
            
        }
        if (parentObject.playerAnswer == answerValue)
        {
            // 明度变暗
            Color darkenedColor = originalColor * 0.5f; // 使颜色变暗
            spriteRenderer.color = darkenedColor;
        }
        else
        {
            // 保持原颜色
            spriteRenderer.color = originalColor;
        }
    }
}
