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
        // Например, вызов смены подуровня при нажатии клавиши
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sublevelManager.SwitchSublevel(1); // Переключение на подуровень с индексом 1
        }
    }
}
