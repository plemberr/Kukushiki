using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject[] objectsToDeactivateInSampleScene;
    public GameObject[] objectsToActivateInPlayScene;
    public int nextSceneIndex; // Индекс следующей сцены в Build Settings

    void OnMouseDown()
    {
        SwitchScenes();
    }

    void SwitchScenes()
    {
        foreach (var obj in objectsToDeactivateInSampleScene)
        {
            obj.SetActive(false);
        }

        foreach (var obj in objectsToActivateInPlayScene)
        {
            obj.SetActive(true);
        }

        SceneManager.LoadScene(nextSceneIndex); // Загрузка следующей сцены по индексу
    }
}

