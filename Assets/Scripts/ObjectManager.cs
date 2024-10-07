using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public AnswerObject[] answerObjects; // 7���𰸶���

    public void CheckAnswers()
    {
        bool allCorrect = true;
        foreach (var obj in answerObjects)
        {
            if (!obj.CompareAnswer())
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("ͨ�����ԣ�");
        }
        else
        {
            Debug.Log("����ʧ�ܡ�");
        }
    }
}
