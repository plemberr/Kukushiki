using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;

    public GameObject[][] sublevelObjects;
    private int currentSublevelIndex = 0;

    public Button switchButton; // Ссылка на кнопку в Unity Editor

    void Start()
    {
        // Инициализация массивов объектов для каждого подуровня
        sublevelObjects = new GameObject[][]
        {
            new GameObject[] { object1, object2, object3 }, // Первый подуровень
            new GameObject[] { object4 },                   // Второй подуровень
            new GameObject[] { object5 }           // Третий подуровень
        };

        // Показываем объекты для текущего подуровня
        SwitchSublevelObjects(currentSublevelIndex);

        // Привязываем метод IncreaseSublevelIndex к событию нажатия на кнопку
        switchButton.onClick.AddListener(IncreaseSublevelIndex);
    }

    public void SwitchSublevelObjects(int sublevelIndex)
    {
        // Скрываем все объекты
        foreach (GameObject[] objs in sublevelObjects)
        {
            foreach (GameObject obj in objs)
            {
                obj.SetActive(false);
            }
        }

        // Показываем объекты для указанного подуровня
        foreach (GameObject obj in sublevelObjects[sublevelIndex])
        {
            obj.SetActive(true);
        }

        // Устанавливаем текущий индекс подуровня
        currentSublevelIndex = sublevelIndex;
    }

    public void IncreaseSublevelIndex()
    {
        // Увеличиваем индекс на 1
        currentSublevelIndex++;

        // Переключаем объекты на следующий подуровень
        if (currentSublevelIndex >= sublevelObjects.Length)
        {
            currentSublevelIndex = 0; // Если достигнут конец списка подуровней, переключаем на первый
        }

        SwitchSublevelObjects(currentSublevelIndex);
    }
}