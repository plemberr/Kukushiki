using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isPlaced = false;

    public Vector3 initialPosition; // Начальная позиция для этого куска пазла
    public Vector3 correctPosition; // Правильная позиция для этого куска пазла
    public float snapThreshold = 0.5f; // Радиус, в котором кусочек "прилипает" к правильной позиции
    public string puzzlePieceName; // Уникальное имя или идентификатор для этого куска пазла

    private void Start()
    {
        // Загружаем сохраненное положение, если оно есть
        if (PlayerPrefs.HasKey(puzzlePieceName + "_x") && PlayerPrefs.HasKey(puzzlePieceName + "_y") && PlayerPrefs.HasKey(puzzlePieceName + "_z"))
        {
            float x = PlayerPrefs.GetFloat(puzzlePieceName + "_x");
            float y = PlayerPrefs.GetFloat(puzzlePieceName + "_y");
            float z = PlayerPrefs.GetFloat(puzzlePieceName + "_z");
            transform.position = new Vector3(x, y, z);

            // Проверяем, если позиция соответствует правильной, то фиксируем её
            if (Vector3.Distance(transform.position, correctPosition) < snapThreshold)
            {
                transform.position = correctPosition;
                isPlaced = true;
            }
        }
        else
        {
            // Устанавливаем начальную позицию для нового кусочка пазла
            transform.position = initialPosition;
        }
    }

    private void OnMouseDown()
    {
        if (!isPlaced)
        {
            // Сохраняем координату Z объекта
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Вычисляем смещение между позицией мыши и позицией объекта
            offset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Получаем координаты мыши в пространстве экрана
        Vector3 mousePoint = Input.mousePosition;

        // Устанавливаем координату Z объекта
        mousePoint.z = zCoord;

        // Конвертируем координаты из экранного пространства в мировое
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (!isPlaced)
        {
            // Обновляем позицию объекта
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        if (!isPlaced)
        {
            float distance = Vector3.Distance(transform.position, correctPosition);


            if (distance < snapThreshold)
            {
                transform.position = correctPosition;
                isPlaced = true; // Закрепляем кусочек на правильной позиции
            }

            // Сохраняем текущее положение
            SavePosition();
        }
    }

    private void SavePosition()
    {
        PlayerPrefs.SetFloat(puzzlePieceName + "_x", transform.position.x);
        PlayerPrefs.SetFloat(puzzlePieceName + "_y", transform.position.y);
        PlayerPrefs.SetFloat(puzzlePieceName + "_z", transform.position.z);
    }
}
