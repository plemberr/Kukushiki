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

    public Button switchButton; // Ссылка на кнопку в Unity Editor

    void Start()
    {
        // Инициализация списка объектов для каждого подуровня
        sublevelObjects = new List<List<GameObject>>
        {
            new List<GameObject> { object1, object2, object3 }, // Первый подуровень
            new List<GameObject> { object4 },                   // Второй подуровень
            new List<GameObject> { object5, object6 }           // Третий подуровень
        };

        // Показываем объекты для текущего подуровня
        SwitchSublevelObjects();

        // Привязываем метод SwitchSublevelObjects к событию нажатия на кнопку
        switchButton.onClick.AddListener(SwitchSublevelObjects);
    }

    public void SwitchSublevelObjects()
    {
        // Проверяем правильность ответа
        if (currentSublevelIndex >= 2 && !string.IsNullOrEmpty(answerInputField.text))
        {
            isAnswerCorrect = IsCorrectAnswer(answerInputField.text);
            if (isAnswerCorrect)
            {
                currentSublevelIndex++;
                if (currentSublevelIndex >= sublevelObjects.Count)
                {
                    // Если достигнут конец списка подуровней, переключаем на первый
                    currentSublevelIndex = 0;
                }
                answerInputField.text = "";
            }
        }

        // Скрываем все объекты
        foreach (List<GameObject> objs in sublevelObjects)
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(false);
            }
        }

        // Показываем объекты для указанного подуровня
        foreach (GameObject obj in sublevelObjects[currentSublevelIndex])
        {
            obj.SetActive(true);
            currentSublevelIndex++;
        }



    }

    private bool IsCorrectAnswer(string answer)
    {
        // Здесь ваша логика проверки правильности ответа
        return answer == "0";
    }
}

