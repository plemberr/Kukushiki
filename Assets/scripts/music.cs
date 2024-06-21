using UnityEngine;

public class Music : MonoBehaviour
{
    public SpriteRenderer musicSpriteRenderer; // ������ �� SpriteRenderer ��� ���������� ���������
    public Sprite onMusic;
    public Sprite offMusic;

    public GameObject musicControlObject; // ������ �� ������� ������ � �����������
    public bool isOn;
    public AudioSource ad;

    private static Music instance;

    void Awake()
    {
        // ���������, ���������� �� ��� ������ Music
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� �������� �� ����� �����

            // ������� ������� ������ ������ �� ������ �����, ���� �� �� ��� ���������� � ����������
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
            Destroy(gameObject); // ���������� �������� �������
            return; // ����� �� ������, ���� ���� ��������
        }
    }

    void Start()
    {
        // ��������� ����������� �������� �����
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicSpriteRenderer.sprite = onMusic; // ������������� ������ ��� ����������� �����
            ad.enabled = true;
            isOn = true;
        }
        else
        {
            musicSpriteRenderer.sprite = offMusic; // ������������� ������ ��� ������������ �����
            ad.enabled = false;
            isOn = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��������� ������� ����� ������ ����
        {
            // ���������, ��� �� ����� ������� ������
            if (musicControlObject != null && IsMouseOverGameObject())
            {
                ToggleSound();
            }
        }
    }

    private bool IsMouseOverGameObject()
    {
        // ���������, ��������� �� ���� ��� ������� ��������
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == musicControlObject;
    }

    public void ToggleSound()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("music", isOn ? 0 : 1);

        // �������� ��� ��������� ����
        ad.enabled = isOn;
        musicSpriteRenderer.sprite = isOn ? onMusic : offMusic; // ������������� ������ � ����������� �� ��������� �����
    }
}







