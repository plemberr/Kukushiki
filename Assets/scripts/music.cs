using UnityEngine;

public class Music : MonoBehaviour
{
    public SpriteRenderer musicSpriteRenderer; // Ссылка на SpriteRenderer для управления спрайтами
    public Sprite onMusic;
    public Sprite offMusic;

    public GameObject musicControlObject; // Ссылка на игровой объект с коллайдером
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

            // Находим игровой объект только на нужной сцене, если он не был установлен в инспекторе
            if (musicControlObject == null)
            {
                musicControlObject = GameObject.Find("MusicControlObject");
                if (musicControlObject == null)
                {
                    Debug.LogError("MusicControlObject not found in the scene!");
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
            musicSpriteRenderer.sprite = onMusic; // Устанавливаем спрайт для включенного звука
            ad.enabled = true;
            isOn = true;
        }
        else
        {
            musicSpriteRenderer.sprite = offMusic; // Устанавливаем спрайт для выключенного звука
            ad.enabled = false;
            isOn = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие левой кнопки мыши
        {
            // Проверяем, был ли нажат игровой объект
            if (musicControlObject != null && IsMouseOverGameObject())
            {
                ToggleSound();
            }
        }
    }

    private bool IsMouseOverGameObject()
    {
        // Проверяем, находится ли мышь над игровым объектом
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == musicControlObject;
    }

    public void ToggleSound()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("music", isOn ? 0 : 1);

        // Включаем или выключаем звук
        ad.enabled = isOn;
        musicSpriteRenderer.sprite = isOn ? onMusic : offMusic; // Устанавливаем спрайт в зависимости от состояния звука
    }
}







