using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public AnswerObject[] answerObjects; // 7个答案对象
    public Text resultText; // 显示结果的UI文本
    private void Start()
    {
        resultText.gameObject.SetActive(false); // 初始时隐藏文本
    }
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
        resultText.gameObject.SetActive(true); // 显示文本
        if (allCorrect)
        {
            resultText.text = "Success!!!";
            resultText.color = Color.green; // 胜利时设置为绿色
        }
        else
        {
            resultText.text = "Failed...";
            resultText.color = Color.red; // 失败时设置为红色
            Invoke("Load", 3f);
        }

    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
