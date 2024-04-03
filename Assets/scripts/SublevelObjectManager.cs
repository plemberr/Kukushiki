using UnityEngine;

public class SublevelObjectManager : MonoBehaviour
{
    public GameObject[] sublevelObjects; // Массив объектов для каждого подуровня
    private int currentSublevelIndex = 0; // Текущий индекс подуровня

    void Start()
    {
        // При старте уровня активируем объекты для первого подуровня
        SetSublevelObjects(currentSublevelIndex);
    }

    public void SwitchSublevel(int sublevelIndex)
    {
        // Вызывается при смене подуровня, передаем индекс нового подуровня
        SetSublevelObjects(sublevelIndex);
    }

    private void SetSublevelObjects(int index)
    {
        // Деактивируем все объекты
        foreach (var obj in sublevelObjects)
        {
            obj.SetActive(false);
        }

        // Активируем объекты для текущего подуровня
        sublevelObjects[index].SetActive(true);

        // Устанавливаем текущий индекс подуровня
        currentSublevelIndex = index;
    }
}
