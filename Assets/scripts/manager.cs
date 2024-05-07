using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class LevelManager : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;
    public GameObject object7;
    public GameObject object8;
    public GameObject object9;
    public GameObject object10;
    public GameObject object11;
    public GameObject object12;
    public GameObject object13;
    public GameObject object14;
    public GameObject object15;
    public GameObject object16;
    public GameObject object17;
    public GameObject object18;
    public GameObject object19;

    public GameObject[][] sublevelObjects;
    private int currentSublevelIndex = 0;

    public UnityEngine.UI.Button switchButton; // ������ �� ������ � Unity Editor
    public TMP_InputField answerInputField; // ���� ��� ����� ������

    public Sprite[] buttonSprites; // ������ �������� ������
    private UnityEngine.UI.Image switchButtonImage; // ������ �� ��������� Image ������

    public string[] correctAnswers = { "answer1", "answer1", "answer1" }; // ���������� ������ ��� ������� ������

    public SceneAsset nextScene;
    public UnityEngine.UI.Image errorImage;
    public UnityEngine.UI.Button closeButton; // ������ ��������

    void Start()
    {
        errorImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

        // ������������� �������� �������� ��� ������� ���������
        sublevelObjects = new GameObject[][]
        {
            new GameObject[] { object1, object2, object3 }, // ������ ����������
            new GameObject[] { object4, object5, object6 },                   // ������ ����������
            new GameObject[] { object7, object8, object18 },                     // ������ ����������
            new GameObject[] { object9, object10, object19},
            new GameObject[] { object11 },
            new GameObject[] { object12, object13 },
            new GameObject[] { object14, object15 },
            new GameObject[] { object16, object17 }
        };

        // �������� ������ �� ��������� Image ������
        switchButtonImage = switchButton.GetComponent<Image>();

        // ���������� ������� ��� �������� ���������
        SwitchSublevelObjects(currentSublevelIndex);

        // ����������� ����� IncreaseSublevelIndex � ������� ������� �� ������
        switchButton.onClick.AddListener(IncreaseSublevelIndex);

        // ����������� ����� OnCloseButtonClick � ������� ������� �� ������ ��������
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public void SwitchSublevelObjects(int sublevelIndex)
    {
        answerInputField.text = "";

        // �������� ��� �������
        foreach (GameObject[] objs in sublevelObjects)
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(false);
            }
        }

        // ���������� ������� ��� ���������� ���������
        foreach (GameObject obj in sublevelObjects[sublevelIndex])
        {
            obj.SetActive(true);
        }

        // ������������� ������� ������ ���������
        currentSublevelIndex = sublevelIndex;

        // ������ ������ ������
        switchButtonImage.sprite = buttonSprites[currentSublevelIndex];
    }

    public void IncreaseSublevelIndex()
    {
        if (currentSublevelIndex >= 5 && !IsCorrectAnswer(answerInputField.text, currentSublevelIndex))
        {
            closeButton.gameObject.SetActive(true);
            errorImage.gameObject.SetActive(true);
            return;
        }

        // ����������� ������ �� 1
        currentSublevelIndex++;

        // ����������� ������� �� ��������� ����������
        if (currentSublevelIndex >= sublevelObjects.Length)
        {
            SceneManager.LoadScene(nextScene.name);
        }

        SwitchSublevelObjects(currentSublevelIndex);
    }

    private bool IsCorrectAnswer(string answer, int num)
    {
        // ���������, ��� num ��������� � ���������� ��������� ��� ������� correctAnswers
        if (num >= 5 && num - 5 < correctAnswers.Length)
        {
            // ���� num � ���������� ���������, ��������� ������������ ������
            return answer == correctAnswers[num - 5];
        }
        else
        {
            // ���� num ��������� �� ��������� ����������� ���������, ������� ����� ������������
            return false;
        }
    }

    public void OnCloseButtonClick()
    {   
        // �������� ����������� ������ ��� ������� �� ������ ��������
        errorImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }
}
