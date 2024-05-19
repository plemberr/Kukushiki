using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private void OnMouseDown()
    {
        // Сохраняем координату Z объекта
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Вычисляем смещение между позицией мыши и позицией объекта
        offset = gameObject.transform.position - GetMouseWorldPos();
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
        // Обновляем позицию объекта
        transform.position = GetMouseWorldPos() + offset;
    }
}
