using UnityEngine;

public class SublevelObjectManager : MonoBehaviour
{
    public GameObject[] sublevelObjects; // ������ �������� ��� ������� ���������
    private int currentSublevelIndex = 0; // ������� ������ ���������

    void Start()
    {
        // ��� ������ ������ ���������� ������� ��� ������� ���������
        SetSublevelObjects(currentSublevelIndex);
    }

    public void SwitchSublevel(int sublevelIndex)
    {
        // ���������� ��� ����� ���������, �������� ������ ������ ���������
        SetSublevelObjects(sublevelIndex);
    }

    private void SetSublevelObjects(int index)
    {
        // ������������ ��� �������
        foreach (var obj in sublevelObjects)
        {
            obj.SetActive(false);
        }

        // ���������� ������� ��� �������� ���������
        sublevelObjects[index].SetActive(true);

        // ������������� ������� ������ ���������
        currentSublevelIndex = index;
    }
}
