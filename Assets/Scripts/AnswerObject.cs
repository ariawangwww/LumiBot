using UnityEngine;

public class AnswerObject : MonoBehaviour
{
    public int realAnswer; // ��ʵ��
    public int playerAnswer; // ��Ҵ�

    public void SetPlayerAnswer(int answer)
    {
        playerAnswer = answer;
    }

    public bool CompareAnswer()
    {
        return playerAnswer == realAnswer;
    }
}
