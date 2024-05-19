using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject object20;

    public GameObject support;

    public GameObject[][] sublevelObjects;
    private int currentSublevelIndex = 0;

    public Button switchButton; // Ссылка на кнопку в Unity Editor
    public TMP_InputField answerInputField; // Поле для ввода ответа

    public Sprite[] buttonSprites; // Массив спрайтов кнопки
    private Image switchButtonImage; // Ссылка на компонент Image кнопки

    public string[] correctAnswers = { "answer1", "answer1", "answer1" }; // Правильные ответы для первого уровня

    public SceneAsset nextScene;
    public Image errorImage;
    public Button closeButton; // Кнопка крестика

    void Start()
    {
        errorImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        support.SetActive(false);

        // Инициализация массивов объектов для каждого подуровня
        sublevelObjects = new GameObject[][]
        {
            new GameObject[] { object1, object2, object3 }, // Первый подуровень
            new GameObject[] { object4, object5, object6 }, // Второй подуровень
            new GameObject[] { object7, object8, object18 }, // Третий подуровень
            new GameObject[] { object9, object10, object19 },
            new GameObject[] { object11 },
            new GameObject[] { object12, object13 },
            new GameObject[] { object14, object15 },
            new GameObject[] { object16, object17 },
            new GameObject[] { object20 }
        };

        // Получаем ссылку на компонент Image кнопки
        switchButtonImage = switchButton.GetComponent<Image>();

        // Показываем объекты для текущего подуровня
        SwitchSublevelObjects(currentSublevelIndex);

        // Привязываем метод IncreaseSublevelIndex к событию нажатия на кнопку
        switchButton.onClick.AddListener(IncreaseSublevelIndex);

        // Привязываем метод OnCloseButtonClick к событию нажатия на кнопку крестика
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public void SwitchSublevelObjects(int sublevelIndex)
    {
        answerInputField.text = "";

        // Скрываем все объекты
        foreach (GameObject[] objs in sublevelObjects)
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(false);
            }
        }

        // Проверка, что sublevelIndex не выходит за пределы массива
        if (sublevelIndex >= 0 && sublevelIndex < sublevelObjects.Length)
        {
            // Показываем объекты для указанного подуровня
            foreach (GameObject obj in sublevelObjects[sublevelIndex])
            {
                obj.SetActive(true);
            }

            // Меняем спрайт кнопки, если индекс в пределах массива buttonSprites
            if (sublevelIndex < buttonSprites.Length)
            {
                switchButtonImage.sprite = buttonSprites[sublevelIndex];
            }
        }

        // Устанавливаем текущий индекс подуровня
        currentSublevelIndex = sublevelIndex;
    }

    public void IncreaseSublevelIndex()
    {
        if ((currentSublevelIndex >= 5 && currentSublevelIndex < 8) && !IsCorrectAnswer(answerInputField.text, currentSublevelIndex))
        {
            closeButton.gameObject.SetActive(true);
            errorImage.gameObject.SetActive(true);
            return;
        }

        // Увеличиваем индекс на 1
        currentSublevelIndex++;

        // Переключаем объекты на следующий подуровень
        if (currentSublevelIndex >= sublevelObjects.Length)
        {
            SceneManager.LoadScene(nextScene.name);
        }
        else
        {
            SwitchSublevelObjects(currentSublevelIndex);
        }
    }

    private bool IsCorrectAnswer(string answer, int num)
    {
        // Проверяем, что num находится в допустимом диапазоне для массива correctAnswers
        if (num >= 5 && num - 5 < correctAnswers.Length)
        {
            // Если num в допустимом диапазоне, проверяем правильность ответа
            return answer == correctAnswers[num - 5];
        }
        else
        {
            // Если num находится за пределами допустимого диапазона, считаем ответ неправильным
            return false;
        }
    }

    public void OnCloseButtonClick()
    {
        // Скрываем изображение ошибки при нажатии на кнопку крестика
        errorImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }
}
