using UnityEngine;

public class YourOtherScript : MonoBehaviour
{
    private SublevelObjectManager sublevelManager;

    void Start()
    {
        sublevelManager = GameObject.Find("SublevelManager").GetComponent<SublevelObjectManager>();
    }

    void Update()
    {
        // ��������, ����� ����� ��������� ��� ������� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sublevelManager.SwitchSublevel(1); // ������������ �� ���������� � �������� 1
        }
    }
}
