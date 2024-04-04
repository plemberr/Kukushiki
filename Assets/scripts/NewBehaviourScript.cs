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
        // Скрыть объекты, которые нужно убрать
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        // Показать объекты, которые нужно показать
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
}

