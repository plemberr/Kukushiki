using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button switchButton;
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    void Start()
    {
        switchButton.onClick.AddListener(OnSwitchButtonClick);
    }

    void OnSwitchButtonClick()
    {
        // ������ �������, ������� ����� ������
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        // �������� �������, ������� ����� ��������
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
}

