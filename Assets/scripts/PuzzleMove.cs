using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isPlaced = false;

    public Vector3 initialPosition; // ��������� ������� ��� ����� ����� �����
    public Vector3 correctPosition; // ���������� ������� ��� ����� ����� �����
    public float snapThreshold = 0.5f; // ������, � ������� ������� "���������" � ���������� �������
    public string puzzlePieceName; // ���������� ��� ��� ������������� ��� ����� ����� �����

    private void Start()
    {
        // ��������� ����������� ���������, ���� ��� ����
        if (PlayerPrefs.HasKey(puzzlePieceName + "_x") && PlayerPrefs.HasKey(puzzlePieceName + "_y") && PlayerPrefs.HasKey(puzzlePieceName + "_z"))
        {
            float x = PlayerPrefs.GetFloat(puzzlePieceName + "_x");
            float y = PlayerPrefs.GetFloat(puzzlePieceName + "_y");
            float z = PlayerPrefs.GetFloat(puzzlePieceName + "_z");
            transform.position = new Vector3(x, y, z);

            // ���������, ���� ������� ������������� ����������, �� ��������� �
            if (Vector3.Distance(transform.position, correctPosition) < snapThreshold)
            {
                transform.position = correctPosition;
                isPlaced = true;
            }
        }
        else
        {
            // ������������� ��������� ������� ��� ������ ������� �����
            transform.position = initialPosition;
        }
    }

    private void OnMouseDown()
    {
        if (!isPlaced)
        {
            // ��������� ���������� Z �������
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // ��������� �������� ����� �������� ���� � �������� �������
            offset = gameObject.transform.position - GetMouseWorldPos();
        }
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
        if (!isPlaced)
        {
            // ��������� ������� �������
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
                isPlaced = true; // ���������� ������� �� ���������� �������
            }

            // ��������� ������� ���������
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
