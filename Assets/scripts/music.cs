using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;

    public Button MusicButton; // ������ �� ������ ���������� �������
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

            // ������� ������ ������ �� ������ �����, ���� ��� �� ���� ����������� � ����������
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
            Destroy(gameObject); // ���������� �������� �������
            return; // ����� �� ������, ���� ���� ��������
        }
    }

    void Start()
    {
        // ��������� ����������� �������� �����
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.image.sprite = onMusic; // ������������� ������ ��� ����������� �����
            ad.enabled = true;
            isOn = true;
        }
        else
        {
            MusicButton.image.sprite = offMusic; // ������������� ������ ��� ������������ �����
            ad.enabled = false;
            isOn = false;
        }

        MusicButton.onClick.AddListener(ToggleSound); // ��������� ��������� �����
    }

    public void ToggleSound()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("music", isOn ? 0 : 1);

        // �������� ��� ��������� ����
        ad.enabled = isOn;
        MusicButton.image.sprite = isOn ? onMusic : offMusic; // ������������� ������ � ����������� �� ��������� �����
    }
}





