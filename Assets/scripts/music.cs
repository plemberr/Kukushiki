using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;

    public Button MusicButton; // Изменено на Button
    public bool isOn;
    public AudioSource ad;

    private static Music instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при переходе на новую сцену
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликат объекта
        }
    }

    void Start()
    {
        // Загружаем сохраненное значение звука
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.image.sprite = onMusic; // Используем image.sprite для установки спрайта
            ad.enabled = true;
            isOn = true;
        }
        else if (PlayerPrefs.GetInt("music") == 1)
        {
            MusicButton.image.sprite = offMusic; // Используем image.sprite для установки спрайта
            ad.enabled = false;
            isOn = false;
        }

        MusicButton.onClick.AddListener(ToggleSound); // Добавляем слушатель клика
    }

    public void ToggleSound()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("music", isOn ? 0 : 1);

        // Включаем или выключаем звук
        ad.enabled = isOn;
        MusicButton.image.sprite = isOn ? onMusic : offMusic; // Используем image.sprite для установки спрайта
    }
}
