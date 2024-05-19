using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private void OnMouseDown()
    {
        // ��������� ���������� Z �������
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // ��������� �������� ����� �������� ���� � �������� �������
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        // �������� ���������� ���� � ������������ ������
        Vector3 mousePoint = Input.mousePosition;

        // ������������� ���������� Z �������
        mousePoint.z = zCoord;

        // ������������ ���������� �� ��������� ������������ � �������
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        // ��������� ������� �������
        transform.position = GetMouseWorldPos() + offset;
    }
}
