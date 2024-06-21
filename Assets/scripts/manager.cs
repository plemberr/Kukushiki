using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Sublevel
{
    public GameObject[] objects;
}

public class manager : MonoBehaviour
{
    public List<Sublevel> sublevels = new List<Sublevel>();
    private int currentSublevelIndex = 0;

    public Button switchButton; // ������ �� ������ � Unity Editor
    public TMP_InputField answerInputField; // ���� ��� ����� ������

    public Sprite[] buttonSprites; // ������ �������� ������
    private Image switchButtonImage; // ������ �� ��������� Image ������

    public string[] correctAnswers; // ���������� ������ ��� ������

    public int answerCheckStartIndexFromEnd = 4; // ������ �������� ������� (�� �����)
    public int answerCheckEndIndexFromEnd = 2; // ����� �������� ������� (�� �����)

    public int puzzleEpisodes = 5; // ������ � ������
    public int puzzleNumber = 1;

    public Image errorImage;
    public Button closeButton; // ������ ��������

    void Start()
    {
        if (errorImage != null)
            errorImage.gameObject.SetActive(false);
        if (closeButton != null)
            closeButton.gameObject.SetActive(false);

        if (switchButton != null)
        {
            // �������� ������ �� ��������� Image ������
            switchButtonImage = switchButton.GetComponent<Image>();

            // ���������� ������� ��� �������� ���������
            SwitchSublevelObjects(currentSublevelIndex);

            // ����������� ����� IncreaseSublevelIndex � ������� ������� �� ������
            switchButton.onClick.AddListener(IncreaseSublevelIndex);
        }

        if (closeButton != null)
        {
            // ����������� ����� OnCloseButtonClick � ������� ������� �� ������ ��������
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
    }

    public void SwitchSublevelObjects(int sublevelIndex)
    {
        if (answerInputField != null)
        {
            answerInputField.text = "";
        }

        // �������� ��� �������
        foreach (Sublevel sublevel in sublevels)
        {
            foreach (GameObject obj in sublevel.objects)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        // ��������, ��� sublevelIndex �� ������� �� ������� ������
        if (sublevelIndex >= 0 && sublevelIndex < sublevels.Count)
        {
            // ���������� ������� ��� ���������� ���������
            foreach (GameObject obj in sublevels[sublevelIndex].objects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // ������ ������ ������, ���� ������ � �������� ������� buttonSprites
            if (sublevelIndex < buttonSprites.Length && switchButtonImage != null)
            {
                switchButtonImage.sprite = buttonSprites[sublevelIndex];
            }
        }

        // ������������� ������� ������ ���������
        currentSublevelIndex = sublevelIndex;
    }

    public void IncreaseSublevelIndex()
    {
        int totalSublevels = sublevels.Count;
        int answerCheckStartIndex = totalSublevels - answerCheckStartIndexFromEnd;
        int answerCheckEndIndex = totalSublevels - answerCheckEndIndexFromEnd;

        if ((currentSublevelIndex >= answerCheckStartIndex && currentSublevelIndex < answerCheckEndIndex) && !IsCorrectAnswer(answerInputField.text, currentSublevelIndex - answerCheckStartIndex))
        {
            if (closeButton != null)
            {
                closeButton.gameObject.SetActive(true);
            }
            if (errorImage != null)
            {
                errorImage.gameObject.SetActive(true);
            }
            return;
        }

        // ����������� ������ �� 1
        currentSublevelIndex++;

        if (currentSublevelIndex == puzzleEpisodes)
        {
            PuzzleManager.AddPuzzlePiece(puzzleNumber);
        }

        // ����������� ������� �� ��������� ����������
        if (currentSublevelIndex >= sublevels.Count)
        {
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            else
            {
                Debug.LogError("Next scene is not set.");
            }
        }
        else
        {
            SwitchSublevelObjects(currentSublevelIndex);
        }
    }

    private bool IsCorrectAnswer(string answer, int num)
    {
        // ��������� ������������ ������ � �������� ������� correctAnswers
        if (num >= 0 && num < correctAnswers.Length)
        {
            return answer == correctAnswers[num];
        }
        else
        {
            return false;
        }
    }

    public void OnCloseButtonClick()
    {
        // �������� ����������� ������ ��� ������� �� ������ ��������
        if (errorImage != null)
        {
            errorImage.gameObject.SetActive(false);
        }
        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(false);
        }
    }
}


