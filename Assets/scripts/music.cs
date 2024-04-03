using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;

    public Button MusicButton; // �������� �� Button
    public bool isOn;
    public AudioSource ad;

    private static Music instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� �������� �� ����� �����
        }
        else
        {
            Destroy(gameObject); // ���������� �������� �������
        }
    }

    void Start()
    {
        // ��������� ����������� �������� �����
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.image.sprite = onMusic; // ���������� image.sprite ��� ��������� �������
            ad.enabled = true;
            isOn = true;
        }
        else if (PlayerPrefs.GetInt("music") == 1)
        {
            MusicButton.image.sprite = offMusic; // ���������� image.sprite ��� ��������� �������
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
        MusicButton.image.sprite = isOn ? onMusic : offMusic; // ���������� image.sprite ��� ��������� �������
    }
}
