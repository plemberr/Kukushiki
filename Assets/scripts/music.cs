using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;

    public Button MusicButton; // Ссылка на кнопку управления музыкой
    public bool isOn;
    public AudioSource ad;

    private static Music instance;

    void Awake()
    {
        // Проверяем, существует ли уже объект Music
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при переходе на новую сцену

            // Находим кнопку только на нужной сцене, если она не была установлена в инспекторе
            if (MusicButton == null)
            {
                MusicButton = FindObjectOfType<Button>();
                if (MusicButton == null)
                {
                    Debug.LogError("MusicButton not found in the scene!");
                }
            }
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликат объекта
            return; // Выйти из метода, если есть дубликат
        }
    }

    void Start()
    {
        // Загружаем сохраненное значение звука
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.image.sprite = onMusic; // Устанавливаем спрайт для включенного звука
            ad.enabled = true;
            isOn = true;
        }
        else
        {
            MusicButton.image.sprite = offMusic; // Устанавливаем спрайт для выключенного звука
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
        MusicButton.image.sprite = isOn ? onMusic : offMusic; // Устанавливаем спрайт в зависимости от состояния звука
    }
}





