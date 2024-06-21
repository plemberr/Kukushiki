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

    public Button switchButton; // Ссылка на кнопку в Unity Editor
    public TMP_InputField answerInputField; // Поле для ввода ответа

    public Sprite[] buttonSprites; // Массив спрайтов кнопки
    private Image switchButtonImage; // Ссылка на компонент Image кнопки

    public string[] correctAnswers; // Правильные ответы для уровня

    public int answerCheckStartIndexFromEnd = 4; // Начало проверки ответов (от конца)
    public int answerCheckEndIndexFromEnd = 2; // Конец проверки ответов (от конца)

    public int puzzleEpisodes = 5; // эпизод с пазлом
    public int puzzleNumber = 1;

    public Image errorImage;
    public Button closeButton; // Кнопка крестика

    void Start()
    {
        if (errorImage != null)
            errorImage.gameObject.SetActive(false);
        if (closeButton != null)
            closeButton.gameObject.SetActive(false);

        if (switchButton != null)
        {
            // Получаем ссылку на компонент Image кнопки
            switchButtonImage = switchButton.GetComponent<Image>();

            // Показываем объекты для текущего подуровня
            SwitchSublevelObjects(currentSublevelIndex);

            // Привязываем метод IncreaseSublevelIndex к событию нажатия на кнопку
            switchButton.onClick.AddListener(IncreaseSublevelIndex);
        }

        if (closeButton != null)
        {
            // Привязываем метод OnCloseButtonClick к событию нажатия на кнопку крестика
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
    }

    public void SwitchSublevelObjects(int sublevelIndex)
    {
        if (answerInputField != null)
        {
            answerInputField.text = "";
        }

        // Скрываем все объекты
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

        // Проверка, что sublevelIndex не выходит за пределы списка
        if (sublevelIndex >= 0 && sublevelIndex < sublevels.Count)
        {
            // Показываем объекты для указанного подуровня
            foreach (GameObject obj in sublevels[sublevelIndex].objects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // Меняем спрайт кнопки, если индекс в пределах массива buttonSprites
            if (sublevelIndex < buttonSprites.Length && switchButtonImage != null)
            {
                switchButtonImage.sprite = buttonSprites[sublevelIndex];
            }
        }

        // Устанавливаем текущий индекс подуровня
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

        // Увеличиваем индекс на 1
        currentSublevelIndex++;

        if (currentSublevelIndex == puzzleEpisodes)
        {
            PuzzleManager.AddPuzzlePiece(puzzleNumber);
        }

        // Переключаем объекты на следующий подуровень
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
        // Проверяем правильность ответа в пределах массива correctAnswers
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
        // Скрываем изображение ошибки при нажатии на кнопку крестика
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


