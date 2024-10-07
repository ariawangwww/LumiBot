using UnityEngine;

public class SubObject : MonoBehaviour
{
    public int answerValue; // 1, 2 �� 3
    public AnswerObject parentObject; // ���ø�����


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
                Debug.Log("ѡ���: " + answerValue);
            }
        }
    }
}
