using UnityEngine;

public class SubObject : MonoBehaviour
{
    public int answerValue; // 1, 2 �� 3
    public AnswerObject parentObject; // ���ø�����
    private SpriteRenderer spriteRenderer; // ����SpriteRenderer
    private Color originalColor; // ԭʼ��ɫ
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // ����ԭʼ��ɫ
        parentObject = GetComponentInParent<AnswerObject>();
    }


    private void Update()
    {
        // ����������Ƿ���
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            // �������Ƿ��ڵ�ǰSprite��
            if (hit != null && hit.gameObject == gameObject)
            {
                parentObject.SetPlayerAnswer(answerValue);
                
            }
            
        }
        if (parentObject.playerAnswer == answerValue)
        {
            // ���ȱ䰵
            Color darkenedColor = originalColor * 0.5f; // ʹ��ɫ�䰵
            spriteRenderer.color = darkenedColor;
        }
        else
        {
            // ����ԭ��ɫ
            spriteRenderer.color = originalColor;
        }
    }
}
