using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TMP_InputField answerInputField;
    public List<List<GameObject>> sublevelObjects;

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;

    private int currentSublevelIndex = 0;
    private bool isAnswerCorrect = false;

    public Button switchButton; // ������ �� ������ � Unity Editor

    void Start()
    {
        // ������������� ������ �������� ��� ������� ���������
        sublevelObjects = new List<List<GameObject>>
        {
            new List<GameObject> { object1, object2, object3 }, // ������ ����������
            new List<GameObject> { object4 },                   // ������ ����������
            new List<GameObject> { object5, object6 }           // ������ ����������
        };

        // ���������� ������� ��� �������� ���������
        SwitchSublevelObjects();

        // ����������� ����� SwitchSublevelObjects � ������� ������� �� ������
        switchButton.onClick.AddListener(SwitchSublevelObjects);
    }

    public void SwitchSublevelObjects()
    {
        // ��������� ������������ ������
        if (currentSublevelIndex >= 2 && !string.IsNullOrEmpty(answerInputField.text))
        {
            isAnswerCorrect = IsCorrectAnswer(answerInputField.text);
            if (isAnswerCorrect)
            {
                currentSublevelIndex++;
                if (currentSublevelIndex >= sublevelObjects.Count)
                {
                    // ���� ��������� ����� ������ ����������, ����������� �� ������
                    currentSublevelIndex = 0;
                }
                answerInputField.text = "";
            }
        }

        // �������� ��� �������
        foreach (List<GameObject> objs in sublevelObjects)
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(false);
            }
        }

        // ���������� ������� ��� ���������� ���������
        foreach (GameObject obj in sublevelObjects[currentSublevelIndex])
        {
            obj.SetActive(true);
            currentSublevelIndex++;
        }



    }

    private bool IsCorrectAnswer(string answer)
    {
        // ����� ���� ������ �������� ������������ ������
        return answer == "0";
    }
}

