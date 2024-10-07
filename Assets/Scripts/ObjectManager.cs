using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public AnswerObject[] answerObjects; // 7个答案对象

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
            Debug.Log("通过测试！");
        }
        else
        {
            Debug.Log("测试失败。");
        }
    }
}
