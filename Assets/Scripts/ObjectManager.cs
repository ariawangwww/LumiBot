using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public AnswerObject[] answerObjects; // 7���𰸶���
    public Text resultText; // ��ʾ�����UI�ı�
    private void Start()
    {
        resultText.gameObject.SetActive(false); // ��ʼʱ�����ı�
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
        resultText.gameObject.SetActive(true); // ��ʾ�ı�
        if (allCorrect)
        {
            resultText.text = "Success!!!";
            resultText.color = Color.green; // ʤ��ʱ����Ϊ��ɫ
        }
        else
        {
            resultText.text = "Failed...";
            resultText.color = Color.red; // ʧ��ʱ����Ϊ��ɫ
            Invoke("Load", 3f);
        }

    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
