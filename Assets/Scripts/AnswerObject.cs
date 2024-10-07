using UnityEngine;

public class AnswerObject : MonoBehaviour
{
    public int realAnswer; // 真实答案
    public int playerAnswer; // 玩家答案

    public void SetPlayerAnswer(int answer)
    {
        playerAnswer = answer;
    }

    public bool CompareAnswer()
    {
        return playerAnswer == realAnswer;
    }
}
